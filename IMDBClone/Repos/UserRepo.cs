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
    }
}
