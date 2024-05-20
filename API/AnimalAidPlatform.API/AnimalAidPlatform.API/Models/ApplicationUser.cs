using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalAidPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public override string PhoneNumber { get; set; }

        public override string Email { get; set; }

        public Role Role { get; set; }

        public int? AnimalShelterId { get; set; }
        public AnimalShelter? RegularWorkingPlace { get; set; }
        public AnimalShelter? AdminWorkingPlace { get; set; }

        public ICollection<FeedPost>? Posts { get; set; }
    }
}