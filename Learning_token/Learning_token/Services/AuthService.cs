using Learning_token.Models;
using Learning_token.Models.Custom;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Learning_token.Services
{
    public class AuthService : IAuthService
    {
        private readonly ProjectDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(string idUser)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1), //pueden ser segundos, minutos, dias, etc...
                SigningCredentials = tokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            //string tokenCreated = tokenHandler.WriteToken(tokenConfig);



            return tokenHandler.WriteToken(tokenConfig);
        }

        public async Task<AuthResponse> GetToken(AuthRequest authentication)
        {
            
            var user = _context.Users.FirstOrDefault(x =>
                x.UserName == authentication.UserName &&
                x.Password == authentication.Password
            );
            
            /*
            var user = new User()
            {
                IdUser = 1,
                UserName = "hola",
                Password = "hola1"

            };
            */

            if ( user == null ) return await Task.FromResult<AuthResponse>(null);

            string token = GenerateToken(user.IdUser.ToString());

            return new AuthResponse() { Token = token, Success = true, Message = "Ok" };
        }
    }
}
