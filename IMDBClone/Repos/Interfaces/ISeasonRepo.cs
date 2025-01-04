using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface ISeasonRepo : IRepo<Season>
    {
        Task<Season> GetLastSeasonAsync(Guid seriesId);
        Task<IEnumerable<Season>> GetAllSeasonsAsync(Guid seriesId);
        Task<Season> GetByIdAsync(Guid id);
    }
}
