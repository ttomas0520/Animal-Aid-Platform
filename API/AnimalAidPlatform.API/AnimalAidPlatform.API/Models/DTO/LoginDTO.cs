using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;    

    }
}
