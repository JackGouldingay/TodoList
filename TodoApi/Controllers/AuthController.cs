using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.Database.Auth;
using TodoApi.Models.General;
using TodoApi.Models.Web.Auth;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RegisterUser([FromBody] RegisterModel registerModel)
        {
            try
            {
                User user = _authService.RegisterUser(registerModel);

                return Ok(new Response(200, "Successfully registered user."));
            } catch(Exception ex)
            {
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to register user.", "").GetError());
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult LoginUser([FromBody] LoginModel loginModel)
        {
            try
            {
                Console.WriteLine("New Login Request");
                JWTToken token = _authService.LoginUser(loginModel);

                Console.WriteLine(token);
                return Ok(new Response(200, "Sucessfully logged in user", token));
            } catch(Exception ex)
            {
                Console.WriteLine(ex.GetType());
                if (ex.GetType() == typeof(Error))
                {
                    Error error = (Error)ex;

                    return StatusCode(error.status, error.GetError());
                }

                return BadRequest(new Error(400, "Unable to login user.", "").GetError());
            }
        }
    }
}
