using IMDBClone.Dtos;
using IMDBClone.Models;

namespace IMDBClone.Services.Interfaces
{
    public interface IMovieManager
    {
        Task<bool> CreateAsync(CreateMovieDto model, Guid adminId);
        Task<bool> UpdateAsync(EditMovieDto model, Guid adminId);
        Task<bool> IsMovieNameUnique(string name, Guid movieId);
        Task<bool> DeleteAsync(Guid id);
        Task<Movie> GetByIdAsync(Guid id);
        Task<IEnumerable<Movie>> GetAllAsync();
    }
}
