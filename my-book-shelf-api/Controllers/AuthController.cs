using Microsoft.AspNetCore.Mvc;
using my_book_shelf_api.Core.Base.Controller;
using my_book_shelf_api.Models;
using my_book_shelf_api.Services;

namespace my_book_shelf_api.Controllers
{    
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }  

        [HttpGet("login")]
        public IActionResult Login([FromQuery] AuthModel auth)
        {
            var res = _authService.Login(auth);
            return Ok(res);
        } 
    }
}
