using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;
using System.Collections.Generic;

namespace IMDBClone.Repos
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        public UserRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            if (userName == null)
                throw new NullReferenceException("UserName should not be null");
            return (await FindAsync(x => x.Name == userName)).ToList().Count() > 0 ? true : false;
        }
    }
}
