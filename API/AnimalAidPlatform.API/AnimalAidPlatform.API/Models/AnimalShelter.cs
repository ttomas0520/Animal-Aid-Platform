using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class AnimalShelter
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A menhely nevének megadása kötelező.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A menhely kapacitásának megadása kötelező.")]
        [Range(0, int.MaxValue, ErrorMessage = "A kapacitás nem lehet negatív.")]
        public int Capacity { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string? Website { get; set; }

        public DateTime EstablishedDate { get; set; }

        public string Description { get; set; }

        public Guid? PersonId { get; set; }
        public ICollection<ApplicationUser> Admins { get; set; }
        public ICollection<ApplicationUser> Workers { get; set; }
    }
}
