namespace TodoApp.Models.Auth
{
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
