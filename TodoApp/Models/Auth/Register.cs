namespace TodoApp.Models.Auth
{
    public class RegisterData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
