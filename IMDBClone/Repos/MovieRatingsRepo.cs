using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class MovieRatingsRepo : Repo<MovieRatings>, IMovieRatingsRepo
    {
        public MovieRatingsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
