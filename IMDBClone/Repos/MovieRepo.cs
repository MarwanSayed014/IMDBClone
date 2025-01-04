﻿using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos.Interfaces;

namespace IMDBClone.Repos
{
    public class MovieRepo : Repo<Movie>, IMovieRepo
    {
        public MovieRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Movie> GetByIdAsync(Guid id)
        {
            return (await FindAsync(x => x.Id == id)).SingleOrDefault();
        }
    }
}
