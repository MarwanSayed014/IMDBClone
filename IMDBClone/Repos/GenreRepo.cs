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
        public async Task<Genre> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }

        public async Task<bool> IsExistsAsync(IEnumerable<Guid> genreIds)
        {
            foreach (var id in genreIds.Distinct().ToList())
            {
                if (await GetByIdAsync(id) == null) return false;
            }
            return true;
        }
    }
}
