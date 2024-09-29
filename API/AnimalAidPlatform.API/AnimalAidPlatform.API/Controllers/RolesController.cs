using AnimalAidPlatform.API.Models.DTO;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO data)
        {
            if (string.IsNullOrEmpty(data.RoleName))
            {
                return BadRequest("Role name is required");
            }

            var roleExist = await _roleManager.RoleExistsAsync(data.RoleName);

            if (roleExist)
            {
                return BadRequest("Role already exist");
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(data.RoleName));

            if (roleResult.Succeeded)
            {
                return Ok(new { message = "Role created" });
            }
            else
            {
                return BadRequest(roleResult.Errors.ToString());
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRolesWithUserCount()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesWithUserCount = new List<RoleDTO>();

            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                var userCount = usersInRole.Count;

                rolesWithUserCount.Add(new RoleDTO
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    UserCount = userCount
                });
            }

            return Ok(rolesWithUserCount);
        }
    }
}
