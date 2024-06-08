namespace TodoApi.Models.Database.Auth
{
    public class JWTToken
    {
        public required string Token { get; set; }
        public required DateTime Expiration { get; set; }
    }
}
