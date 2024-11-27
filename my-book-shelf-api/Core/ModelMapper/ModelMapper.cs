using my_book_shelf_api.Models.Dto;
using my_book_shelf_api.Models;
using AutoMapper;

namespace my_book_shelf_api.Core.ModelMapper
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
