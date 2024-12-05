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
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public UserService(DataContext context, IMapper mapper, AuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<bool> Create(User user)
        {
            user.Password = _authService.HashPassword(user.Password);
            user.CreateDate = DateTime.Now;
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
