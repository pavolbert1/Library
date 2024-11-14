using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IRepository
{
    public interface IUserRepository
    {
        Task InsertUser(User user);

        IEnumerable<User> GetAllUsers();

        Task<User?> GetUserDetails(int id);

        Task UpdateUser(User book);

        Task DeleteUser(int id);
    }
}