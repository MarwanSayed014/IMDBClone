using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IEpisodeRepo : IRepo<Episode>
    {
        Task<Episode> GetByIdAsync(Guid id);
        Task<IEnumerable<Episode>> GetAllEpisodesAsync(Guid seasonId);
        Task<Episode> GetLastEpisodeAsync(Guid seasonId);

    }
}
