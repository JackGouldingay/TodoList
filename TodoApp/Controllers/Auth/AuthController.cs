using Microsoft.AspNetCore.Mvc;
using System.Data;
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
                dynamic data = response.Data;
                ViewData["SuccessMessage"] = response.Message;

                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Parse((string)data.expiration);
                cookieOptions.Path = "/";
                Response.Cookies.Append("token", (string)data.token);
                
                return Redirect("~/Home");
            } catch(Exception ex)
            {
                if(ex.GetType() == typeof(ApiResponseError))
                {
                    ApiResponseError error = (ApiResponseError)ex;
                    ViewData["Errors"] = new List<string>() { error.ApiMessage };

                    return View();
                }
            }
            return View();
        }
    }
}
