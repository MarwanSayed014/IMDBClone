using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IProducerManager
    {
        Task<bool> CreateAsync(CreateProducerDto model, Guid adminId);
        Task<bool> UpdateAsync(EditProducerDto model, Guid adminId);
        Task<bool> IsProducerNameUnique(string name, Guid ProducerId);
        Task<bool> DeleteAsync(Guid id);
        Task<Producer> GetByIdAsync(Guid id);
        Task<IEnumerable<Producer>> GetAllAsync();
    }
}
