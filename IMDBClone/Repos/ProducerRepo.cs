using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class ProducerRepo : Repo<Producer>, IProducerRepo
    {
        public ProducerRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Producer> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }
    }
}
