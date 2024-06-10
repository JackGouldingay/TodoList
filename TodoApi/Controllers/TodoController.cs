using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TodoApi.Models.Database.Todo;
using TodoApi.Models.General;
using TodoApi.Models.Web.Todo;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoService _todoService;
        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("Create")]
        public ActionResult CreateTodo(CreateTodoModel todoModel)
        {
            try
            {
                TodoItem todoItem = _todoService.CreateTodo(todoModel);

                return Ok(new Response(200, "Sucessfully created a todo", todoItem));
            } catch(Exception ex)
            {
                if(ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to create todo item.", "").GetError());

            }
        }

        [HttpGet("all")]
        public ActionResult GetTodos(string ownerId)
        {
            try
            {
                IEnumerable<TodoItem> todoItem = _todoService.GetTodoItems(Guid.Parse(ownerId));

                return Ok(new Response(200, "Successfully found todos", todoItem));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to create todo item.", "").GetError());

            }
        }

        [HttpGet]
        public ActionResult GetTodo(string id)
        {
            try
            {
                TodoItem todoItem = _todoService.GetTodoItem(Guid.Parse(id));

                return Ok(new Response(200, "Successfully found todo", todoItem));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to create todo item.", "").GetError());

            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateTodo(Guid id, [FromBody] UpdateTodoModel updateTodoModel)
        {
            try
            {
                TodoItem todoItem =_todoService.UpdateTodo(id, updateTodoModel);
                
                return Ok(new Response(200, "Updated todo item", todoItem));
            } catch(Exception ex)
            {
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to create todo item.", "").GetError());
            }
        }

        [HttpDelete]
        public ActionResult DeleteTodo(Guid id)
        {
            try
            {
                bool deletion = _todoService.DeleteTodo(id);
                if (!deletion) throw new Error(400, "Unable to delete todo item", "");

                return Ok(new Response(200, "Successfully deleted todo item."));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to create todo item.", "").GetError());
            }
        }
    }
}
