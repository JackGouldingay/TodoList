using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models.Web.Todo
{
    public class CreateTodoModel
    {
        [Required(ErrorMessage = "Name is required")]
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Owner ID is required")]
        [JsonPropertyName("ownerId")]
        public required Guid OwnerId { get; set; }
        [JsonPropertyName("completed")]
        public bool Completed { get; set; } = false;
    }
}
