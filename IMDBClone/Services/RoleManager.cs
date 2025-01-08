using IMDBClone.Models;
using IMDBClone.Repos;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.EntityFrameworkCore;
using XAct.Users;

namespace IMDBClone.Services
{
    public class RoleManager : IRoleManager
    {
        public IUserRolesRepo _userRolesRepo { get; }
        public IRoleRepo _roleRepo { get; }


        public RoleManager(IUserRolesRepo userRolesRepo, IRoleRepo roleRepo)
        {
            _userRolesRepo = userRolesRepo;
            _roleRepo = roleRepo;
        }
        public async Task AddUserToRoleAsync(Guid userId, string roleName)
        {
            await _userRolesRepo.CreateAsync(new UserRoles
            {
                RoleId = (Guid)((await _roleRepo.GetRoleAsync(roleName))?.Id),
                UserId = userId
            });
            await _userRolesRepo.SaveAsync();
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId)
        {
            return (await _userRolesRepo.FindAsync(x => x.UserId == userId)).Include(x => x.Role).Select(x => x.Role).ToList();
        }
    }
}
