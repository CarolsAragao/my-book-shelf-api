using my_book_shelf_api.Models.Dto;
using my_book_shelf_api.Models;
using AutoMapper;

namespace my_book_shelf_api.Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UserCreate, User>();

        CreateMap<BookDto, Book>();
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Cover, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Cover)
                    ? "https://thumbs.dreamstime.com/z/open-book-hand-drawn-illustration-vector-graphic-sketch-literary-volume-219191827.jpg?ct=jpeg"
                    : src.Cover)); 

        CreateMap<BookDtoCreate, Book>();
        CreateMap<BookDtoUpdate, Book>();
    }
}
