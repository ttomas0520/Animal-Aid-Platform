namespace AnimalAidPlatform.API.Models.DTO
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; } = string.Empty;
        public bool IsSucces {  get; set; }
        public string? Message { get; set; }
    }
}
