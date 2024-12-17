using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class GenreRepo : Repo<Genre>, IGenreRepo
    {
        public GenreRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
