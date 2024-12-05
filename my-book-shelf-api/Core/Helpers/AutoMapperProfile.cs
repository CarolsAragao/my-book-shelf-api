using my_book_shelf_api.Models.Dto;
using my_book_shelf_api.Models;
using AutoMapper;

namespace my_book_shelf_api.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
