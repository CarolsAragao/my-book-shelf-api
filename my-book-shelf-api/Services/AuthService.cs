using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using my_book_shelf_api.Core.Base.Model;
using my_book_shelf_api.Models;
using my_book_shelf_api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace my_book_shelf_api.Services
{
    public class AuthService
    {
        private UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserRepository UserRepository, IConfiguration configuration)
        {
            _userRepository = UserRepository;
            _configuration = configuration;
        }

        public ApiResponse<string> Login(AuthModel auth)
        {
            var userToken = ValidateUserCredentials(auth);
            var tokenJWT = string.Empty;

            if (userToken is not null)
            {
                tokenJWT = GenerateJwtToken(userToken);
                return new ApiResponse<string>(true, "Sucesso", tokenJWT);
            }
            return new ApiResponse<string>(false, "Erro", tokenJWT);
        }

        public Token ValidateUserCredentials(AuthModel auth)
        {
            var user = _userRepository.GetUser(auth);

            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(null, user.Password, auth.Password);

            if (user != null && result == PasswordVerificationResult.Success && user.Email == auth.Email)
            {
                return user;
            }
            return null;
        }

        private string GenerateJwtToken(Token userToken)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Guid", userToken.Guid.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userToken.Name),
                new Claim(JwtRegisteredClaimNames.Email, userToken.Email),
                new Claim("CPF", userToken.CPF),
                new Claim("UserType", userToken.UserType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();

            string hashedPassword = passwordHasher.HashPassword(null, password);

            return hashedPassword;
        }
    }
}
