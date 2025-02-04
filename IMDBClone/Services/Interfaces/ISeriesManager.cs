using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface ISeriesManager
    {
        Task<bool> CreateAsync(CreateSeriesDto model, Guid adminId);
        Task<bool> UpdateAsync(EditSeriesDto model, Guid adminId);
        Task<bool> IsSeriesNameUnique(string name, Guid SeriesId);
        Task<bool> DeleteAsync(Guid id);
        Task<Series> GetByIdAsync(Guid id);
        Task<IEnumerable<Series>> GetAllAsync();
    }
}
