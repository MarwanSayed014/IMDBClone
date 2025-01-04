using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IActorRepo : IRepo<Actor>
    {
        Task<Actor> GetByIdAsync(Guid id);
    }
}
