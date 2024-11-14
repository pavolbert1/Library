using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Models;
using LibraryAPI.Core.Exceptions;

namespace LibraryAPI.Infrastructure.Services
{
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task CreateBook(Book book)
        {
            if (GetAllBooks().Any(x => x.Isbn.Equals(book.Isbn)))
            {
                throw new BadRequestException("Book with same ISBN was already added.");
            }

            await _bookRepository.InsertBook(book);
        }

        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAllBooks();

        public async Task<Book?> GetBookDetails(int id) => await _bookRepository.GetBookDetails(id);

        public async Task UpdateBook(Book book) => await _bookRepository.UpdateBook(book);

        public async Task DeleteBook(int id) => await _bookRepository.DeleteBook(id);
    }
}