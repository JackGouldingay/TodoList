using System.Text.Json.Serialization;

namespace TodoApp.Models
{
    public class ApiResponse : Exception
    {
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string ApiMessage { get; set; }

        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            ApiMessage = message;
        }
    }
}
