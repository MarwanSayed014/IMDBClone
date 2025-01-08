using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;

namespace IMDBClone.Services
{
    public class UserManager : IUserManager
    {
        public IUserRepo _userRepo { get; }

        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task CreateUserAsync(User user)
        {
            await _userRepo.CreateAsync(user);
            await _userRepo.SaveAsync();
        }

        public async Task<User> GetUserAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;
            return (await _userRepo.FindAsync(x => x.Name == userName &&
                           x.Password == password)).SingleOrDefault();
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _userRepo.UserNameExistsAsync(userName);
        }
    }
}
