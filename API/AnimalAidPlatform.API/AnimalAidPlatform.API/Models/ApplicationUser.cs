﻿using AnimalAidPlatform.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        public override string PhoneNumber { get; set; }

        public override string Email { get; set; }

        public Role Role { get; set; }

        public Guid? AnimalShelterId { get; set; }
        public AnimalShelter? RegularWorkingPlace { get; set; }
        public AnimalShelter? AdminWorkingPlace { get; set; }

        public ICollection<FeedPost>? Posts { get; set; }
    }
}