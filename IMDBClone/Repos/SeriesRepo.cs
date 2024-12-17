using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;

namespace IMDBClone.Repos
{
    public class SeriesRepo : Repo<Series>, ISeriesRepo
    {
        public SeriesRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
