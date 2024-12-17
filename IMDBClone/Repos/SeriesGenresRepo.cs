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
    }
}
