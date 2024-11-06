using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book_shelf_api.Services;

namespace my_book_shelf_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _authService.getAuth();
            return Ok(res);
        }
    }
}
