using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IProducerRepo : IRepo<Producer>
    {
        Task<Producer> GetByIdAsync(Guid id);
    }
}
