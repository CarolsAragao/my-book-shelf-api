using AutoMapper;
using Microsoft.EntityFrameworkCore;
using my_book_shelf_api.Core.Base.Model;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using my_book_shelf_api.Models.Dto;

namespace my_book_shelf_api.Services;

public class UserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<bool>> Create(UserCreate userCreate)
    {
        var usermapped = _mapper.Map<User>(userCreate);
        _context.Users.Add(usermapped);
        var res = await _context.SaveChangesAsync() > 0;

        if (res)
        {
            return Result<bool>.Ok();
        } 

        return Result<bool>.Fail("Error to create new User");
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        var res = await _context.Users.ToListAsync();
        var resMapped = _mapper.Map<IEnumerable<UserDto>>(res);
        return resMapped;
    }

    public User GetUserByEmail(string email)
    {
        var res = _context.Users.Where(user => user.Email == email).FirstOrDefault();
        return res;
    }

}
