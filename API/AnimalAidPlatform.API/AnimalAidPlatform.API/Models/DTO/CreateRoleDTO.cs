using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage ="Role Name is required")]
        public string RoleName { get; set; } = null!;

    }
}
