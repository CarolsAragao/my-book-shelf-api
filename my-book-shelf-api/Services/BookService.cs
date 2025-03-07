using AutoMapper;
using Microsoft.EntityFrameworkCore;
using my_book_shelf_api.Core.Base.Model;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using my_book_shelf_api.Models.Dto;

namespace my_book_shelf_api.Services
{
    public class BookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BookDto>>> Get()
        {
            var res =  await _context.Books.ToListAsync();

            var resMapped = _mapper.Map<IEnumerable<BookDto>>(res);

            if(resMapped is null)
            {
                return Result<IEnumerable<BookDto>>.Fail("Could find any Book");
            }

            return Result<IEnumerable<BookDto>>.Ok(resMapped, "");
        }

        public async Task<Result<BookDto>> GetBookById(Guid id)
        {
            var res = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            var bookMapped = _mapper.Map<BookDto>(res);

            if(res is null)
            {
                return Result<BookDto>.Fail("Could find any Book");
            }

            return Result<BookDto>.Ok(bookMapped, "");
        }

        public async Task<Result<bool>> Create(BookDtoCreate bookDtoCreate)
        {
            var mappedBook = _mapper.Map<Book>(bookDtoCreate);

            _context.Books.Add(mappedBook);

            var res = await _context.SaveChangesAsync();

            if(res == 0)
            {
                return Result<bool>.Fail("Not Created");
            }

            return Result<bool>.Ok();
        }

        public async Task<Result<bool>> Update(BookDtoUpdate bookDtoUpdate)
        {
            var mappedBook =  _mapper.Map<Book>(bookDtoUpdate);

            _context.Books.Update(mappedBook);

            var res = await _context.SaveChangesAsync();

            if (res == 0)
            {
                return Result<bool>.Fail("Not updated");
            }

            return Result<bool>.Ok();
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if(book is null)
            {
                return Result<bool>.Fail("Not Deleted");
            }

            _context.Remove(book);
            var res = await _context.SaveChangesAsync();

            return Result<bool>.Ok();
        }
    }
}
