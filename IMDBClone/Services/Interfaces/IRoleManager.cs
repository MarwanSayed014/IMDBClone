using IMDBClone.Models;
using IMDBClone.Types;

namespace IMDBClone.Services.Interfaces
{
    public interface IRoleManager
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);
        Task AddUserToRoleAsync(Guid userId, string roleName);
    }
}
