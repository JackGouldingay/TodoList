using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoApi.Database;
using TodoApi.Models.Database.Auth;
using TodoApi.Models.Database.Todo;
using TodoApi.Models.General;
using TodoApi.Models.Web.Todo;

namespace TodoApi.Services
{
    public class TodoService
    {
        private TodoListDBContext _dbContext;
        private DbSet<TodoItem> _todoItems;
        private AuthService _authService;
        public TodoService(TodoListDBContext dbContext, AuthService authService)
        {
            _dbContext = dbContext;
            _todoItems = _dbContext.TodoItems;
            _authService = authService;
        }

        public TodoItem CreateTodo(CreateTodoModel todoModel)
        {
            try
            {
                User? ownerExists = _authService.GetUserById(todoModel.OwnerId);
                if (ownerExists == null) throw new Error(400, "The owner does not exist.", "");

                TodoItem todoItem = new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Name = todoModel.Name,
                    Description = todoModel.Description,
                    OwnerId = todoModel.OwnerId,
                    Completed = todoModel.Completed
                };

                _todoItems.Add(todoItem);
                _dbContext.SaveChanges();

                return todoItem;

            } catch(Exception ex)
            {
                throw new Error(401, "Unable to create TodoItem.", "");
            }
        }

        public TodoItem UpdateTodo(Guid todoId, UpdateTodoModel updatedTodoModel)
        {
            try
            {
                var entity = this.GetTodoItem(todoId);
                if (entity == null) throw new Error(404, $"No TodoItem exists with the id {todoId}", "Todo/NotExists");

                entity.Name = updatedTodoModel.Name;
                entity.Description = updatedTodoModel.Description;
                entity.Completed = updatedTodoModel.Completed;

                _todoItems.Update(entity);
                _dbContext.SaveChanges();

                return entity;
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new Error(401, "Unable to update TodoItem", "");
            }
        }

        public bool DeleteTodo(Guid todoId)
        {
            try
            {
                var todoItem = this.GetTodoItem(todoId);
                if (todoItem == null) throw new Error(404, "TodoItem does not exist", "");

                _dbContext.Remove(todoItem);
                _dbContext.SaveChanges();

                return true;
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw new Error(401, "Unable to delete TodoItem", "");
            }
        }

        public IEnumerable<TodoItem> GetTodoItems(Guid ownerId)
        {
            try
            {
                var query = from todoItem in _todoItems where todoItem.OwnerId == ownerId select todoItem;
                var result = query.ToList();
                return result;
            } catch (Exception ex)
            {
                throw new Error(401, "Unable to fetch TodoItems.", "");

            }
        }
        
        public TodoItem? GetTodoItem(Guid todoId)
        {
            var query = from todoItem in _todoItems where todoItem.Id == todoId select todoItem;
            return query.FirstOrDefault();
        }
    }
}
