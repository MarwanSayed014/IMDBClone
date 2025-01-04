using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class SeriesRatingsRepo : Repo<SeriesRatings>, ISeriesRatingsRepo
    {
        public SeriesRatingsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
