using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IRepository
{
    public interface IBookRepository
    {
        Task InsertBook(Book book);

        IEnumerable<Book> GetAllBooks();

        Task<Book?> GetBookDetails(int id);

        Task UpdateBook(Book book);

        Task DeleteBook(int id);
    }
}