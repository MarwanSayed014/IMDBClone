using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface ISeriesRepo : IRepo<Series>
    {
        public abstract event SeriesCreatedEventHandler SeriesCreated;
        public delegate Task SeriesCreatedEventHandler(Series series);
        Task<Series> GetByIdAsync(Guid id);
    }
}
