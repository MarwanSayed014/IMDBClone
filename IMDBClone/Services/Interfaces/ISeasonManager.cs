using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface ISeasonManager
    {
        Task<bool> CreateNewSeasonAsync(CreateSeasonDto model);
        Task<bool> UpdateLastAsync(EditSeasonDto model);
        Task<bool> DeleteLastSeasonAsync(Guid seriesId);
        Task<Season> GetByIdAsync(Guid id);
        Task<IEnumerable<Season>> GetAllSeasonsAsync(Guid seriesId);
    }
}
