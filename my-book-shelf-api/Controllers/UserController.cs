using Microsoft.AspNetCore.Mvc;
using my_book_shelf_api.Core.Base.Controller;
using my_book_shelf_api.Models;
using my_book_shelf_api.Services;

namespace my_book_shelf_api.Controllers;

public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreate userCreate)
    {
        var res = await _userService.Create(userCreate);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await _userService.Get();
        return Ok(res);
    }
}
