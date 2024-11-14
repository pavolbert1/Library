using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Models;
using LibraryAPI.Core.Exceptions;

namespace LibraryAPI.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task CreateUser(User user)
        {
            if (GetAllUsers().Any(x => x.Email.Equals(user.Email)))
            {
                throw new BadRequestException("Email address is already used.");
            }

            await _userRepository.InsertUser(user);
        }

        public IEnumerable<User> GetAllUsers() => _userRepository.GetAllUsers();

        public async Task<User?> GetUserDetails(int id) => await _userRepository.GetUserDetails(id);

        public async Task UpdateUser(User user) => await _userRepository.UpdateUser(user);

        public async Task DeleteUser(int id) => await _userRepository.DeleteUser(id);
    }
}