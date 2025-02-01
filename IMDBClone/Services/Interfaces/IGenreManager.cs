using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IGenreManager
    {
        Task<bool> CreateAsync(CreateGenreDto model, Guid adminId);
        Task<bool> UpdateAsync(EditGenreDto model, Guid adminId);
        Task<bool> IsGenreNameUnique(string name, Guid GenreId);
        Task<bool> DeleteAsync(Guid id);
        Task<Genre> GetByIdAsync(Guid id);
        Task<IEnumerable<Genre>> GetAllAsync();
    }
}
