using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models.Database.Todo
{
    public class TodoItem : SaveConfig
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid OwnerId { get; set; }
        public bool Completed { get; set; }
    }
}
