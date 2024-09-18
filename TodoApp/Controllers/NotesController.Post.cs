using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Models.Auth;
using TodoApp.Models.Notes;

namespace TodoApp.Controllers;

public partial class NotesController
{
    [HttpPost]
    public async Task<IActionResult> CreateNote(CreateNote noteData)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["Errors"] = errors;

                return View();
            }
                
            noteData.OwnerId = Request.Cookies.FirstOrDefault(c => c.Key == "userId").Value;
            ApiResponse response = (ApiResponse) await _apiService.PostRequest("/Todo/Create", noteData);
            dynamic data = response.Data;
            ViewData["SuccessMessage"] = response.Message;
            
                
            return Redirect("~/Notes");
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

    public async Task<IActionResult> UpdateNote(Note note)
    {
        Console.WriteLine(note);
        return Redirect("~/Notes");
    }
}