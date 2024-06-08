namespace TodoApi.Models.Configuration
{
    public class JWTTokenSettings
    {
        public string? ValidAudience { get; set; } = null!;
        public string? ValidIssuer { get; set; } = null!;
        public string? Secret { get; set; } = null!;
    }
}
