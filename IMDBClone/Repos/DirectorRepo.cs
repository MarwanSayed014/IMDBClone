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
    }
}
