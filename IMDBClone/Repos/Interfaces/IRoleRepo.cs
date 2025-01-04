using IMDBClone.Models;

namespace IMDBClone.Repos.Interfaces
{
    public interface IRoleRepo : IRepo<Role>
    {
        Task<bool> RoleNameExistsAsync(string roleName);
        Task<Role> GetRoleAsync(string roleName);
    }
}
