using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class SeriesGenresRepo : Repo<SeriesGenres>, ISeriesGenresRepo
    {
        public SeriesGenresRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task AddSeriesToGenresAsync(Guid seriesId, IEnumerable<Guid> genresIds)
        {
            foreach (var id in genresIds)
                await CreateAsync(new SeriesGenres
                {
                    SeriesId = seriesId,
                    GenreId = id
                });
        }

        public async Task RemoveSeriesFromAllGenresAsync(Guid seriesId)
        {
            var movirGenres = (await FindAsync(x => x.SeriesId == seriesId)).ToList();
            foreach (var item in movirGenres)
            {
                await DeleteAsync(item);
            }
        }
    }
}
