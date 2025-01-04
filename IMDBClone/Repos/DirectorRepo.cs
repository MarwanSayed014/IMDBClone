using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using System.IO;

namespace IMDBClone.Repos
{
    public class DirectorRepo : Repo<Director>, IDirectorRepo
    {
        public DirectorRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Director> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }
    }
}
