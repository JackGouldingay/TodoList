using Microsoft.AspNetCore.Mvc;
using TodoApp.Configuration;
using TodoApp.Models;
using TodoApp.Models.Auth;
using TodoApp.Services;

namespace TodoApp.Controllers.Auth
{
    public class AuthController : Controller
    {
        private APIService apiService;
        public AuthController(APIService _apiService)
        {
            apiService = _apiService;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginData loginData)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["Errors"] = errors;

                return View();
            }
            try
            {
                ApiResponse response = (ApiResponse)await apiService.PostRequest("/auth/login", loginData);
                ViewData["SuccessMessage"] = response.ApiMessage;
                Console.WriteLine(response);
            } catch(Exception ex)
            {
                if(ex.GetType() == typeof(ApiResponse))
                {
                    ApiResponse error = (ApiResponse)ex;
                    ViewData["Errors"] = new List<string>() { error.ApiMessage };

                    return View();
                }
            }
            
            return View();
        }
    }
}
