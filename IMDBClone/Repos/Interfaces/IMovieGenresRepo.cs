using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IMovieGenresRepo : IRepo<MovieGenres>
    {
        Task AddMovieToGenresAsync(Guid movieId, IEnumerable<Guid> genresIds);
        Task RemoveMovieFromAllGenresAsync(Guid movieId);
    }
}
