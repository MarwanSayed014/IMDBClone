using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IDirectorRepo : IRepo<Director>
    {
        Task<Director> GetByIdAsync(Guid id);
    }
}
