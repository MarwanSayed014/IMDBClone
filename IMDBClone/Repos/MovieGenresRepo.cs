using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class MovieGenresRepo : Repo<MovieGenres>, IMovieGenresRepo
    {
        public MovieGenresRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
