using System.Text.Json.Serialization;

namespace TodoApi.Models.Database.Auth
{
    public class UserDTO : SaveConfig
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}
