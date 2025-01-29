using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IActorManager
    {
        Task<bool> CreateAsync(CreateActorDto model, Guid adminId);
        Task<bool> UpdateAsync(EditActorDto model, Guid adminId);
        Task<bool> IsActorNameUnique(string name, Guid actorId);
        Task<bool> DeleteAsync(Guid id);
        Task<Actor> GetByIdAsync(Guid id);
        Task<IEnumerable<Actor>> GetAllAsync();
    }
}
