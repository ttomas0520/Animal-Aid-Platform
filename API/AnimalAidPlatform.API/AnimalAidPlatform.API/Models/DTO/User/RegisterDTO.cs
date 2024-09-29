﻿using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO.User
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<string> Roles { get; set; }
    }
}
