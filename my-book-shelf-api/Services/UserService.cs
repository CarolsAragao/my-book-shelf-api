using AutoMapper;
using Microsoft.EntityFrameworkCore;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using my_book_shelf_api.Models.Dto;

namespace my_book_shelf_api.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly Mapper _mapper;

        public UserService(DataContext context, Mapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Create(User user)
        {
            _context.Users.Add(user);
            var res = await _context.SaveChangesAsync() > 0;
            return res;
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var res = await _context.Users.ToListAsync();
            var resMapped = _mapper.Map<IEnumerable<UserDto>>(res);
            return resMapped;
        }
    }
}
