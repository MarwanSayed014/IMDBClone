using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IEpisodeManager
    {
        Task<bool> CreateAsync(CreateEpisodeDto model, Guid adminId);
        Task<bool> UpdateAsync(EditEpisodeDto model, Guid adminId);
        Task<bool> DeleteLastEpisodeAsync(Guid seasonId);
        Task<Episode> GetByIdAsync(Guid id);
        Task<IEnumerable<Episode>> GetAllEpisodesAsync(Guid seasonId);
    }
}
