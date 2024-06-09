using System.Text.Json.Serialization;

namespace TodoApp.Models.Notes
{
    public class Note
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }
        [JsonPropertyName("completed")]
        public Boolean Completed { get; set; }
    }
}
