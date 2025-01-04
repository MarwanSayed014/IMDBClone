using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using static IMDBClone.Repos.Interfaces.ISeriesRepo;

namespace IMDBClone.Repos
{
    public class SeriesRepo : Repo<Series>, ISeriesRepo
    {
        public event SeriesCreatedEventHandler SeriesCreated;

        public SeriesRepo(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<Series> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }

        public new async Task CreateAsync(Series series)
        {
            await base.CreateAsync(series);
            await SeriesCreated?.Invoke(series);
        }
    }
}
