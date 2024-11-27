namespace my_book_shelf_api.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public UserType UserType { get; set; }
    }
}
