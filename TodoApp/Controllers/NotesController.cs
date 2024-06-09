using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TodoApp.Models;
using TodoApp.Models.Notes;
using TodoApp.Services;

namespace TodoApp.Controllers
{
	public class NotesController : Controller
	{
		private APIService _apiService;
		public NotesController(APIService apiService)
		{
			_apiService = apiService;
		}

		public async Task<IActionResult> Index()
		{
			if (!Request.Cookies.ContainsKey("token"))
			{
				return Redirect("~/Home");
			}

			string token = Request.Cookies.First(cookie => cookie.Key == "token").Value;
			
			try
			{
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

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Note note)
		{
            if (!Request.Cookies.ContainsKey("token"))
            {
                return Redirect("~/Home");
            }

			string token = Request.Cookies.First(cookie => cookie.Key == "token").Value;
            

			try
			{
                ApiResponse response = (ApiResponse)await _apiService.GetRequest($"/auth/verify?token={token}");
                string userId = response.Data.userId;
				note.OwnerId = userId;

                ApiResponse noteResponse = (ApiResponse)await _apiService.PostRequest("/todo/create", note);
				dynamic data = noteResponse.Data;
				ViewData["SuccessMessage"] = noteResponse.Message;

				return Redirect("~/notes");
			} catch (Exception ex) {
                if (ex.GetType() == typeof(ApiResponseError))
                {
                    ApiResponseError error = (ApiResponseError)ex;
                    ViewData["Errors"] = new List<string>() { error.ApiMessage };

                    return View();
                }
            }

			return View();
		}

		public async Task<IActionResult> Note(string id)
		{
			try
			{
				ApiResponse noteResponse = (ApiResponse)await _apiService.GetRequest($"/todo?id={id}");
				Note note = (Note)noteResponse.Data;

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
