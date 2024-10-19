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
            var animalShelter =  new AnimalShelter
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
                Donations = this.Donations,
                Location = new NetTopologySuite.Geometries.Point(this.Location.Longitude, this.Location.Latitude)
            };
            animalShelter.Location.SRID = 4326;
            return animalShelter;
        }

        public void UpdateEntity(AnimalShelter existingShelter)
        {
            existingShelter.Name = this.Name;
            existingShelter.Type = this.Type;
            existingShelter.Description = this.Description;
            existingShelter.GeoLong = this.Location.Longitude;
            existingShelter.GeoLat = this.Location.Latitude;
            existingShelter.Address = this.Location.Address;
            existingShelter.PhoneNumber = this.PhoneNumber;
            existingShelter.Email = this.Email;
            existingShelter.Website = this.Website;
            existingShelter.ContactName = this.ContactName;
            existingShelter.ContactPosition = this.ContactPosition;
            existingShelter.Weekdays = this.Weekdays;
            existingShelter.Weekend = this.Weekend;
            existingShelter.Adoption = this.Adoption;
            existingShelter.Visiting = this.Visiting;
            existingShelter.Volunteering = this.Volunteering;
            existingShelter.MedicalCare = this.MedicalCare;
            existingShelter.Donations = this.Donations;
            existingShelter.Location = new NetTopologySuite.Geometries.Point(this.Location.Longitude, this.Location.Latitude)
            {
                SRID = 4326
            };
        }

    }



}
