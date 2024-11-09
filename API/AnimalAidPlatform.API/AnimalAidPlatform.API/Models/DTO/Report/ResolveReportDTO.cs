using AnimalAidPlatform.API.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnimalAidPlatform.API.Models.DTO.Report
{
    public class ResolveReportDTO
    {
        [Required(ErrorMessage = "A válasz megadása kötelező, ha a jelentést lezárjuk.")]
        [StringLength(500, ErrorMessage = "A válasz legfeljebb 500 karakter lehet.")]
        public string? AdminResponse { get; set; } 

        [Required]
        public bool IsResolved { get; set; }

        [Required]
        [DefaultValue(ReportAction.None)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReportAction reportAction { get; set; }
    }

}
