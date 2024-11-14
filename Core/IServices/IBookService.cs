using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IServices
{
    public interface IBookService
    {
        Task CreateBook(Book book);

        IEnumerable<Book> GetAllBooks();

        Task<Book?> GetBookDetails(int id);

        Task UpdateBook(Book book);

        Task DeleteBook(int id);
    }
}