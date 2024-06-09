using System.Text.Json.Serialization;

namespace TodoApp.Models.Notes
{
    public class Note
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("completed")]
        public Boolean Completed { get; set; }
    }
}
