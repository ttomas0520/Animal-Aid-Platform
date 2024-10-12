namespace AnimalAidPlatform.API.Models.DTO
{
    using AnimalAidPlatform.Models;
    using System.ComponentModel.DataAnnotations;

    public class AnimalShelterDTO
    {
        public int Id { get; set; }

        // Shelter Info
        [Required(ErrorMessage = "A menhely nevének megadása kötelező.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A menhely típusának megadása kötelező.")]
        public string Type { get; set; }

        public string? Description { get; set; }

        // Location Info (Using LocationDTO)
        public LocationDTO Location { get; set; }

        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [RegularExpression(@"^[\d\+\-\.\(\)\/\s]*$", ErrorMessage = "Érvénytelen telefonszám formátum.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Az email cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím.")]
        public string Email { get; set; }

        public string? Website { get; set; }

        // Contact Info
        [Required(ErrorMessage = "A kapcsolattartó nevének megadása kötelező.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "A kapcsolattartó pozíciójának megadása kötelező.")]
        public string ContactPosition { get; set; }

        // Opening Hours
        public string? Weekdays { get; set; }
        public string? Weekend { get; set; }

        // Services
        public bool Adoption { get; set; }
        public bool Visiting { get; set; }
        public bool Volunteering { get; set; }
        public bool MedicalCare { get; set; }
        public bool Donations { get; set; }

        public AnimalShelter ToEntity()
        {
            return new AnimalShelter
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Description = this.Description,
                GeoLong = this.Location.Longitude,
                GeoLat = this.Location.Latitude,
                Address = this.Location.Address,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                Website = this.Website,
                ContactName = this.ContactName,
                ContactPosition = this.ContactPosition,
                Weekdays = this.Weekdays,
                Weekend = this.Weekend,
                Adoption = this.Adoption,
                Visiting = this.Visiting,
                Volunteering = this.Volunteering,
                MedicalCare = this.MedicalCare,
                Donations = this.Donations
            };
        }
    }

   

}
