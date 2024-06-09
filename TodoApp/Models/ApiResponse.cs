using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApp.Models
{
    public class ApiResponseError : Exception
    {
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string ApiMessage { get; set; }

        public ApiResponseError(int statusCode, string message)
        {
            StatusCode = statusCode;
            ApiMessage = message;
        }
    }

    public class ApiResponse
    {
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("data")]
        public dynamic Data { get; set; }

        //public ApiResponse(int status, string message, object data)
        //{
        //    StatusCode = status;
        //    ApiMessage = message;
        //    Data = data;
        //}
    }
}
