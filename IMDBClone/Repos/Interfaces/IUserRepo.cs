using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        Task<bool> UserNameExistsAsync(string userName);
    }
}
