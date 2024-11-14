using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.Models;

namespace LibraryAPI.Infrastructure.Repository
{
    public class UserRepository(LibraryDbContext context) : IUserRepository
    {
        private readonly LibraryDbContext _context = context;

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> GetAllUsers() => _context.Users;

        public async Task<User?> GetUserDetails(int id) => await _context.Users.FindAsync(id);

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}