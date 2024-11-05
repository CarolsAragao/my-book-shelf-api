using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_book_shelf_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(true);
        }
    }
}
