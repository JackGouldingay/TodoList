using System.Text.Json.Serialization;

namespace TodoApi.Models.Database.Auth
{
    public class User : SaveConfig
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;


        public UserDTO UserToDTO()
        {
            return new UserDTO
            {
                Id = Id,
                Email = Email,
                Name = Name
            };
        }
    }
}
