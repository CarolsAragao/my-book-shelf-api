using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        
        public string Login(AuthModel auth)
        {
            auth.Password = HashPassword(auth.Password);
            var user = ValidateUserCredentials(auth);
            var token = string.Empty;

            if (user is not null)
            {
                token = GenerateJwtToken(user);
            }
            return token;
        }

        public UserModel ValidateUserCredentials(AuthModel auth)
        {
            var user = _userRepository.GetUser(auth);

            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(null, HashPassword(auth.Password), auth.Password);

            if (user != null && result == PasswordVerificationResult.Success && user.Email == auth.Email)
            {
                return user;
            }
            return null;
        }

        private string GenerateJwtToken(UserModel user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
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
