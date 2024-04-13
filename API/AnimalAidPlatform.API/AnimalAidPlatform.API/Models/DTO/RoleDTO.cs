namespace AnimalAidPlatform.API.Models.DTO
{
    public class RoleDTO
    {
        public string Id { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int UserCount { get; set; }
    }
}
