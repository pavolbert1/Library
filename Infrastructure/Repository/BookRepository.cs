using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.Models;

namespace LibraryAPI.Infrastructure.Repository
{
    public class BookRepository(LibraryDbContext context) : IBookRepository
    {
        private readonly LibraryDbContext _context = context;

        public async Task InsertBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Book> GetAllBooks() => _context.Books;

        public async Task<Book?> GetBookDetails(int id) => await _context.Books.FindAsync(id);

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}