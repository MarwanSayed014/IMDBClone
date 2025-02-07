using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.EventHandlers
{
    public class SeriesEventHandler
    {
        public ISeasonRepo _seasonRepo { get; }

        public SeriesEventHandler(ISeasonRepo seasonRepo)
        {
            _seasonRepo = seasonRepo;
        }

        public async Task CreateFirstSeason(Series series)
        {
            await _seasonRepo.CreateAsync(new Season
            {
                Number = 1,
                ReleaseYear = series.ReleaseYear,
                SeriesId = series.Id,
                SeasonPrequelId = null
            });
            await _seasonRepo.SaveAsync();
        }
    }
}
