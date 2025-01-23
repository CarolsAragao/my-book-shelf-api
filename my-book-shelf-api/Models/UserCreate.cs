using Microsoft.AspNetCore.Identity;
using my_book_shelf_api.Core.Base.Model;

namespace my_book_shelf_api.Models;

public class UserCreate : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CPF { get; set; }
    public UserType UserType { get; set; }
    public bool Active { get; set; }

    public UserCreate()
    {
        CreateDate = DateTime.Now;
        Password = HashPassword(Password);
    }

    public string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<object>();
        string hashedPassword = passwordHasher.HashPassword(null, password);

        return hashedPassword;
    }
}    
