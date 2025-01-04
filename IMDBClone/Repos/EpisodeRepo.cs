using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class EpisodeRepo : Repo<Episode>, IEpisodeRepo
    {
        public EpisodeRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Episode>> GetAllEpisodesAsync(Guid seasonId)
        {
            return (await FindAsync(x => x.SeasonId == seasonId)).ToList();
        }

        public async Task<Episode> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }

        public async Task<Episode> GetLastEpisodeAsync(Guid seasonId)
        {
            return (await FindAsync(x => x.SeasonId == seasonId)).
                OrderByDescending(x => x.Number).FirstOrDefault();
        }
    }
}
