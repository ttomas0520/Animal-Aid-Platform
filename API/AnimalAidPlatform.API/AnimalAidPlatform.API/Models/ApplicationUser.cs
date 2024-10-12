using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public override string PhoneNumber { get; set; }

        public override string Email { get; set; }

        public Role Role { get; set; }

        public ICollection<FeedPost>? Posts { get; set; }
        public NotificationSettings? NotificationSettings { get; set; }
    }
}