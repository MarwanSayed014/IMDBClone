using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface ISeriesGenresRepo : IRepo<SeriesGenres>
    {
        Task AddSeriesToGenresAsync(Guid SeriesId, IEnumerable<Guid> genresIds);
        Task RemoveSeriesFromAllGenresAsync(Guid SeriesId);
    }
}
