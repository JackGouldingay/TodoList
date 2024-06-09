using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using TodoApp.Configuration;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class APIService
    {
        private ConfigurationModel config;

        public APIService(IOptions<ConfigurationModel> _config)
        {
            config = _config.Value;
        }

        public async Task<object?> PostRequest(string url, object _data)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(config.APIURL);
                var dataToSend = JsonConvert.SerializeObject(_data);
                var stringData = new StringContent(dataToSend, Encoding.UTF8, @"application/json");
                var result = await client.PostAsync($"api{url}", stringData);

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);
                    var response = JsonConvert.DeserializeObject<ApiResponse>(resultContent);
                    
                    return response;
                }
                else
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    ApiResponseError error = JsonConvert.DeserializeObject<ApiResponseError>(resultContent);

                    throw error;
                }
            }
        }

		public async Task<object?> GetRequest(string url, object _data)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(config.APIURL);
				var dataToSend = JsonConvert.SerializeObject(_data);
				var stringData = new StringContent(dataToSend, Encoding.UTF8, @"application/json");
				var result = await client.GetAsync($"api{url}");

				if (result.IsSuccessStatusCode)
				{
					string resultContent = await result.Content.ReadAsStringAsync();
					Console.WriteLine(resultContent);
					var response = JsonConvert.DeserializeObject<ApiResponse>(resultContent);

					return response;
				}
				else
				{
					string resultContent = await result.Content.ReadAsStringAsync();
					ApiResponseError error = JsonConvert.DeserializeObject<ApiResponseError>(resultContent);

					throw error;
				}
			}
		}
	}
}
