using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IServices
{
    public interface IUserService
    {
        Task CreateUser(User user);

        IEnumerable<User> GetAllUsers();

        Task<User?> GetUserDetails(int id);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}