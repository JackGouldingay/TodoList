using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TodoApp.Models;
using TodoApp.Models.Notes;
using TodoApp.Services;

namespace TodoApp.Controllers
{
	public partial class NotesController : Controller
	{
		private APIService _apiService;
		public NotesController(APIService apiService)
		{
			_apiService = apiService;
		}
		
		public async Task<IActionResult> Index()
		{
			try
			{
				string token = Request.Cookies.First(cookie => cookie.Key == "token").Value;
				
				ApiResponse response = (ApiResponse)await _apiService.GetRequest($"/auth/verify?token={token}");
				string userId = response.Data.userId;
				ApiResponse noteResponse = (ApiResponse)await _apiService.GetRequest($"/todo/all?ownerId={userId}");
				JArray data = (JArray)noteResponse.Data;
				
				List<Note> notes = data.ToObject<List<Note>>();
				
				ViewData["Notes"] = notes;
				ViewData["SuccessMessage"] = noteResponse.Message;

				return View();
			}
			catch (Exception ex)
			{
				if (ex.GetType() == typeof(ApiResponseError))
				{
					ApiResponseError error = (ApiResponseError)ex;
					ViewData["Errors"] = new List<string>() { error.ApiMessage };

					return View();
				}
			}

			return View();
		}

		public IActionResult CreateNote()
		{
			return View();
		}
		
		public async Task<IActionResult> Note(string id)
		{
			try
			{
				ApiResponse noteResponse = (ApiResponse)await _apiService.GetRequest($"/todo?id={id}");
				Note note = noteResponse.Data;

				ViewData["Note"] = note;

				return View();
			}
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ApiResponseError))
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
