using Microsoft.AspNetCore.Mvc;
using my_book_shelf_api.Core.Base.Controller;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using my_book_shelf_api.Models.Dto;
using my_book_shelf_api.Services;

namespace my_book_shelf_api.Controllers
{
    public class BookController : BaseController
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _bookService.Get();
            return Ok(res);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var res = await _bookService.GetBookById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDtoCreate bookDtoCreate)
        {
            var res = await _bookService.Create(bookDtoCreate);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookDtoUpdate bookDtoUpdate)
        {
            var res = await _bookService.Update(bookDtoUpdate);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var res = await _bookService.Delete(id);
            return Ok(res);
        }
    }
}
