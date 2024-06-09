using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
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
                var dataToSend = JsonSerializer.Serialize(_data);
                var stringData = new StringContent(dataToSend, Encoding.UTF8, @"application/json");
                var result = await client.PostAsync($"api{url}", stringData);

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);
                    object responseJson = JsonSerializer.Deserialize<object>(resultContent);

                    return responseJson;
                }
                else
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    ApiResponse apiError = JsonSerializer.Deserialize<ApiResponse>(resultContent);


                    throw apiError;
                }
            }
        }
    }
}
