using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class UserRolesRepo : Repo<UserRoles>, IUserRolesRepo
    {
        public UserRolesRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
