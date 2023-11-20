using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A fajta megadása kötelező.")]
        public string Breed { get; set; }

        public byte[] Photo { get; set; } // Kép bináris adatként (byte tömbként)

        [Required(ErrorMessage = "A kor megadása kötelező.")]
        [Range(0, int.MaxValue, ErrorMessage = "A kor nem lehet negatív.")]
        public int Age { get; set; }

        public string Description { get; set; }

        public int AnimalShelterId { get; set; }
        public AnimalShelter AnimalShelter { get; set; }
    }
}
