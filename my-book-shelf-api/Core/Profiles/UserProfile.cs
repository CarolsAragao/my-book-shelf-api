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
    }
}
