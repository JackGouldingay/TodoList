using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Database;
using TodoApi.Models.Configuration;
using TodoApi.Models.Database.Auth;
using TodoApi.Models.General;
using TodoApi.Models.Web.Auth;
using static BCrypt.Net.BCrypt;

namespace TodoApi.Services
{
    public class AuthService
    {
        private TodoListDBContext _context;
        private DbSet<User> _users;
        private JWTService _jwtService;

        public AuthService(TodoListDBContext context, JWTService jWTService)
        {
            _context = context;
            _users = context.Users;
            _jwtService = jWTService;
        }

        public User RegisterUser(RegisterModel registerModel)
        {
            try
            {
                var dbUser = this.GetUserByEmail(registerModel.Email);
                if (dbUser != null)
                {
                    throw new Error(400, "User exists with the email specified", "User/email");
                }

                var password = HashPassword(registerModel.Password);

                var user = new User
                {
                    Name = registerModel.Name,
                    Email = registerModel.Email,
                    Password = password
                };

                _users.Add(user);
                _context.SaveChanges();

                return user;
            } catch(Exception ex)
            {
                throw new Error(400, "Unable to register user.", "");
            }
        }

        public JWTToken LoginUser(LoginModel loginModel)
        {
            try
            {
                User? user = this.GetUserByEmail(loginModel.Email);

                if (user == null || !Verify(loginModel.Password, user.Password))
                    throw new Error(401, "Email or Password is incorrect.", "");

                var claims = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email)
                };

                JWTToken token = _jwtService.Generate(claims);

                return token;
            } catch(Exception ex)
            {
                throw new Error(401, "Unable to login user.", "");
            }
        }

        public User? GetUserByEmail(string email)
        {
            return (from b in _users where b.Email.Equals(email) select b).FirstOrDefault();
        }

        public User? GetUserById(Guid id)
        {
            return (from b in _users where b.Id.Equals(id) select b).FirstOrDefault();
        }
    }
}
