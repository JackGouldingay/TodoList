using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models.Web.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
