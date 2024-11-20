namespace my_book_shelf_api.Models
{
    public class Token
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string UserType { get; set; }
    }
}
