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
            var user = _userRepository.GetUser(auth);
            
            if (user is null) return new ApiResponse<string>(false, "Usuário não encontrado.", "");

            var validatedUser = ValidateUserCredentials(auth, user);

            if (validatedUser)
            {
                var tokenJWT = GenerateJwtToken(user);
                return new ApiResponse<string>(true, "Credenciais validadas.", tokenJWT);
            }

            return new ApiResponse<string>(false, "Credenciais inválidas.", "");
        }

        public bool ValidateUserCredentials(AuthModel auth, User user)
        {           
            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(null, user.Password, auth.Password);

            if (result == PasswordVerificationResult.Success && user.Email == auth.Email) return true;

            return false;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Guid", user.Guid.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("CPF", user.CPF),
                new Claim("UserType", user.UserType),
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
