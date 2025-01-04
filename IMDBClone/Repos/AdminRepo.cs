using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class AdminRepo : Repo<Admin>, IAdminRepo
    {
        public AdminRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
