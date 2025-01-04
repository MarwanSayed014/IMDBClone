using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IMovieRepo : IRepo<Movie>
    {
        Task<Movie> GetByIdAsync(Guid id);
    }
}
