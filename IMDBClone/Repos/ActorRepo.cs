using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class ActorRepo : Repo<Actor>, IActorRepo
    {
        public ActorRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Actor> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x=> x.Id == id)).SingleOrDefault();
        }
    }
}
