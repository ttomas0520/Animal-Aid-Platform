using AnimalAidPlatform.API.Models.DTO.User;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimalAidPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Email = data.Email,
                Name = data.Name,
                UserName = data.Email,
                PhoneNumber = data.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (data.Roles is null)
            {
                await _userManager.AddToRoleAsync(user, "REGULAR");
            }
            else
            {
                await _userManager.AddToRolesAsync(user, data.Roles);
            }
            return Ok(new AuthResponseDTO
            {
                IsSucces = true,
                Message = "Account created sucessfully "
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user is null)
            {
                return Unauthorized(new AuthResponseDTO { IsSucces = false, Message = "User not found with this email" });
            }

            var result = await _userManager.CheckPasswordAsync(user, data.Password);

            if (!result)
            {
                return Unauthorized(new AuthResponseDTO { IsSucces = false, Message = "Invalid Password" });

            }

            var token = GenerateToken(user);
            return Ok(new AuthResponseDTO
            {
                Token = token,
                IsSucces = true,
                Message = "Login Success"
            });
        }

        private string GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings").GetSection("securityKey").Value!);

            var roles = _userManager.GetRolesAsync(user).Result;
            List<Claim> claims = [
                    new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(JwtRegisteredClaimNames.Name, user.Name ?? ""),
                new(JwtRegisteredClaimNames.NameId, user.Id ?? ""),
                new(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWTSettings").GetSection("validAudience").Value!),
                new(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWTSettings").GetSection("validIssuer").Value!)
                ];
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet("detail")]
        public async Task<ActionResult<UserDetailDTO>> GetUserDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId!);

            if(user is null)
            {
                return NotFound(new AuthResponseDTO
                {
                    IsSucces=false,
                    Message = "User not found"
                });
            }

            return Ok(new UserDetailDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Roles= [..await _userManager.GetRolesAsync(user)],
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount =user.AccessFailedCount,
                TwoFactorEnabled = user.TwoFactorEnabled,
            });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetUser()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDetails = new List<UserDetailDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDetails.Add(new UserDetailDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles.ToArray()
                });
            }

            return Ok(userDetails);
        }

    }
}
