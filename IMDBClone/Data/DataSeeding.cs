using IMDBClone.Models;
using IMDBClone.Repos;
using IMDBClone.Repos.Interfaces;
using IMDBClone.Types;

namespace IMDBClone.Data
{
    public class DataSeeding
    {
        private IRoleRepo _roleRepo { get; }

        public DataSeeding(IRoleRepo repo)
        {
            _roleRepo = repo;
        }
        public async Task SeedAsync()
        {
            try
            {
                if (!(await _roleRepo.RoleNameExistsAsync(RoleTypes.User.ToString())))
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleTypes.User.ToString()
                    };
                    await _roleRepo.CreateAsync(role);
                    await _roleRepo.SaveAsync();
                }
                if (!(await _roleRepo.RoleNameExistsAsync(RoleTypes.Admin.ToString())))
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleTypes.Admin.ToString()
                    };
                    await _roleRepo.CreateAsync(role);
                    await _roleRepo.SaveAsync();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
