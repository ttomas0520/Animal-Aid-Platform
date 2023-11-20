using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.Models
{
    public class FeedPost
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A tartalom megadása kötelező.")]
        public string ContentText { get; set; }
        public string ImageUrl {  get; set; }
        public string UrlHandle {  get; set; }

        public DateTime PostDate { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<Animal> AssociatedAnimals { get; set; }

    }
}
