namespace TodoApp.Models.Notes;

public class CreateNote
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? OwnerId { get; set; }
    public Boolean Completed { get; set; }
}