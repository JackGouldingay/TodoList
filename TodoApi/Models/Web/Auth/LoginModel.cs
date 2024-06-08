using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models.Web.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }
}
