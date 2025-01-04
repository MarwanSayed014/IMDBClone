using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IGenreRepo : IRepo<Genre>
    {
        Task<Genre> GetByIdAsync(Guid id);
        Task<bool> IsExistsAsync(IEnumerable<Guid> genreIds);
    }
}
