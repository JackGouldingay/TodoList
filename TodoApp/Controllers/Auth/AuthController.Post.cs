using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Models.Auth;

namespace TodoApp.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginData loginData)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["Errors"] = errors;

                return View();
            }
                
            ApiResponse response = (ApiResponse)await apiService.PostRequest("/auth/login", loginData);
            dynamic data = response.Data;
            ViewData["SuccessMessage"] = response.Message;

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.ParseExact((string)data.expiration, "M/d/yyyy H:mm:ss", CultureInfo.InvariantCulture);
            cookieOptions.Path = "/";
            Response.Cookies.Append("token", (string)data.token);

            ApiResponse userIdResponse = (ApiResponse)await apiService.GetRequest($"/auth/verify?token={data.token}");
            dynamic userIdData = userIdResponse.Data;
            
            Response.Cookies.Append("userId", (string)userIdData.userId);
            
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
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterData registerData)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["Errors"] = errors;

                return View();
            }
                
            ApiResponse response = (ApiResponse)await apiService.PostRequest("/auth/register", registerData);
            dynamic data = response.Data;
            ViewData["SuccessMessage"] = response.Message;
                
            return Redirect("~/Auth/Login");
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