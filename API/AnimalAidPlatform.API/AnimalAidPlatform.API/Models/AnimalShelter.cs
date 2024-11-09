using AnimalAidPlatform.API.Models.DTO;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class AnimalShelter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A menhely nevének megadása kötelező.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A menhely típusának megadása kötelező.")]
        public string Type { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public double GeoLong { get; set; }
        public double GeoLat { get; set; }
        public string Address { get; set; }
        public string LocUrl { get; set; }

        public Point? Location { get; set; }

        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [RegularExpression(@"^[\d\+\-\.\(\)\/\s]*$", ErrorMessage = "Érvénytelen telefonszám formátum.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Az email cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím.")]
        public string Email { get; set; }

        public string? Website { get; set; }

        [Required(ErrorMessage = "A kapcsolattartó nevének megadása kötelező.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "A kapcsolattartó pozíciójának megadása kötelező.")]
        public string ContactPosition { get; set; }

        public string? Weekdays { get; set; }
        public string? Weekend { get; set; }

        public bool Adoption { get; set; }
        public bool Visiting { get; set; }
        public bool Volunteering { get; set; }
        public bool MedicalCare { get; set; }
        public bool Donations { get; set; }

        public AnimalShelterDTO ToDto()
        {
            return new AnimalShelterDTO
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Description = this.Description,
                Location = new LocationDTO
                {
                    Longitude = this.GeoLong,
                    Latitude = this.GeoLat,
                    Address = this.Address,
                    Url = this.LocUrl
                },
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
