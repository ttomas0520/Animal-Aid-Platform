﻿using AnimalAidPlatform.API.Models.Enums;

namespace AnimalAidPlatform.API.Models.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Urlhandle { get; set; }
    }
}
