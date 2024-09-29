using AnimalAidPlatform.Models;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
namespace AnimalAidPlatform.API.Models
{
    public class FeedPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A tartalom megadása kötelező.")]
        public string ContentText { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public double GeoLong { get; set; }
        public double GeoLat { get; set; }
        public string Address { get; set; }

        public Point Location { get; set; }

        public DateTime PostDate { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public ICollection<Animal>? AssociatedAnimals { get; set; }

    }
}
