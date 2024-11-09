using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO.Report
{
    public class ReportDTO
    {
        [Required(ErrorMessage = "A poszt azonosító megadása kötelező.")]
        public int FeedPostId { get; set; }

        [Required(ErrorMessage = "A jelentés indoklása kötelező.")]
        [StringLength(500, ErrorMessage = "A jelentés indoklása legfeljebb 500 karakter lehet.")]
        public string Reason { get; set; }
    }

}
