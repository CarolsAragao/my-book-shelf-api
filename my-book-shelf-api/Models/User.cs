using my_book_shelf_api.Core.Base.Model;

namespace my_book_shelf_api.Models;

public enum UserType
{
    ADMIN = 1,
    USER = 2
}
public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string CPF { get; set; }
    public UserType UserType { get; set; }
    public bool Active { get; set; }
}    
