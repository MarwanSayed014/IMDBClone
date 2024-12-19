using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class RoleRepo : Repo<Role>, IRoleRepo
    {
        public RoleRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> RoleNameExistsAsync(string roleName)
        {
            if (roleName == null)
                return false;
            return (await FindAsync(x => x.Name == roleName)).ToList().Count() > 0 ? true : false;
        }
        public async Task<Role> GetRoleAsync(string roleName)
        {
            if (roleName == null)
                throw new NullReferenceException("RoleName should not be null");
            return (await FindAsync(x => x.Name == roleName)).FirstOrDefault();
        }
    }
}
