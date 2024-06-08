namespace TodoApi.Models.Web.Todo
{
    public class UpdateTodoModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Completed { get; set; }
    }
}
