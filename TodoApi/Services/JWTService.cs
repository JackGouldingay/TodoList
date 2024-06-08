using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.Models.Configuration;
using TodoApi.Models.Database.Auth;

namespace TodoApi.Services
{
    public class JWTService
    {
        private JWTTokenSettings _JWTSettings;
        
        public JWTService(IOptions<JWTTokenSettings> jwtTokenSettings)
        {
            _JWTSettings = jwtTokenSettings.Value;
        }

        public JWTToken Generate(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTSettings.Secret!));
            var expiration = DateTime.Now.AddDays(30);
            var token = new JwtSecurityToken(
                    issuer: _JWTSettings.ValidIssuer,
                    audience: _JWTSettings.ValidAudience,
                    expires: expiration,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JWTToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

            return jwtToken;
        }

        public Guid Verify(string token)
        {
            if (token == null) return Guid.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_JWTSettings.Secret!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;

                var userId = claims.FirstOrDefault(x => x.Type == "Id");
                if (userId == null) return Guid.Empty;

                Console.WriteLine($"Verifed user {userId}");

                return Guid.Parse(userId.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JWTService {ex.Message}");
                // return null if validation fails
                return Guid.Empty;
            }
        }
    }
}
