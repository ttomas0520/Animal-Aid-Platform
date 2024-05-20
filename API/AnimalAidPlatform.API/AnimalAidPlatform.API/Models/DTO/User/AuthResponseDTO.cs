namespace AnimalAidPlatform.API.Models.DTO.User
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; } = string.Empty;
        public bool IsSucces { get; set; }
        public string? Message { get; set; }
    }
}
