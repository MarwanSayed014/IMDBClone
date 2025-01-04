using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class SeasonRepo : Repo<Season>, ISeasonRepo
    {
        public SeasonRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Season> GetLastSeasonAsync(Guid seriesId)
        {
            return (await FindAsync(x=> x.SeriesId == seriesId)).
                OrderByDescending(x=> x.Number).FirstOrDefault();
        }
        public async Task<IEnumerable<Season>> GetAllSeasonsAsync(Guid seriesId)
        {
            return (await FindAsync(x=> x.SeriesId == seriesId)).ToList();
        }
        public async Task<Season> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }
    }
}
