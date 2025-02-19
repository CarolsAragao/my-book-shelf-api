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

        public async Task<ApiResponse<IEnumerable<BookDto>>> Get()
        {

            var res =  await _context.Books.ToListAsync();

            var resMapped = _mapper.Map<IEnumerable<BookDto>>(res);

            if(resMapped is null)
            {
                return new ApiResponse<IEnumerable<BookDto>>(false, "Could find any Book", new List<BookDto>());
            }

            return new ApiResponse<IEnumerable<BookDto>>(true, "", resMapped);
        }

        public async Task<ApiResponse<BookDto>> GetBookById(Guid id)
        {
            var res = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            var bookMapped = _mapper.Map<BookDto>(res);

            if(res is null)
            {
                return new ApiResponse<BookDto> (false, "Could find any Book", new BookDto());
            }

            return new ApiResponse<BookDto>(true, "", bookMapped);
        }

        public async Task<ApiResponse<bool>> Create(BookDtoCreate bookDtoCreate)
        {
            var mappedBook = _mapper.Map<Book>(bookDtoCreate);

            _context.Books.Add(mappedBook);

            var res = await _context.SaveChangesAsync();

            if(res == 0)
            {
                return new ApiResponse<bool>(false, "Not Created", false);
            }

            return new ApiResponse<bool>(true, "Book Created", true);
        }

        public async Task<ApiResponse<bool>> Update(BookDtoUpdate bookDtoUpdate)
        {
            var mappedBook =  _mapper.Map<Book>(bookDtoUpdate);

            _context.Books.Update(mappedBook);

            var res = await _context.SaveChangesAsync();

            if (res == 0)
            {
                return new ApiResponse<bool>(false, "Not updated", false);
            }

            return new ApiResponse<bool>(true, "Book updated", true);
        }

        public async Task<ApiResponse<bool>> Delete(Guid id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if(book is null)
            {
                return new ApiResponse<bool>(false, "Not deleted", false);
            }

            _context.Remove(book);
            var res = await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, "Book deleted!!!", true);
        }
    }
}
