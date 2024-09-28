namespace AnimalAidPlatform.API.Options
{
    public class GmailOptions
    {
        public const string GmaiOptionsKey = "GmailOptions";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
