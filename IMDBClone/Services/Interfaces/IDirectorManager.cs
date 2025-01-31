using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IDirectorManager
    {
        Task<bool> CreateAsync(CreateDirectorDto model, Guid adminId);
        Task<bool> UpdateAsync(EditDirectorDto model, Guid adminId);
        Task<bool> IsDirectorNameUnique(string name, Guid DirectorId);
        Task<bool> DeleteAsync(Guid id);
        Task<Director> GetByIdAsync(Guid id);
        Task<IEnumerable<Director>> GetAllAsync();
    }
}
