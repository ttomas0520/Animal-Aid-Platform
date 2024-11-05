using AnimalAidPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int FeedPostId { get; set; }  // Link to the reported post
        public FeedPost FeedPost { get; set; }

        [Required]
        public string ReporterId { get; set; }  // User who reported the post
        public ApplicationUser Reporter { get; set; }

        [Required(ErrorMessage = "A jelentés indoklása kötelező.")]
        public string Reason { get; set; }  // Explanation of the report

        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
        public bool IsResolved { get; set; } = false;
        public string? AdminResponse { get; set; }  // Optional response from admin
    }

}
