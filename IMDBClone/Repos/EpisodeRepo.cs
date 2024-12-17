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
    }
}
