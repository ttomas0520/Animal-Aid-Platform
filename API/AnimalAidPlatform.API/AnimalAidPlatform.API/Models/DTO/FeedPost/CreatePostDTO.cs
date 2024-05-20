using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO.FeedPost
{
    public class CreatePostDTO
    {

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A tartalom megadása kötelező.")]
        public string ContentText { get; set; }
        public LocationDTO Location { get; set; }
        public string? ShortAddess { get; set; }

        public int CategoryId {  get; set; }
    }
}
