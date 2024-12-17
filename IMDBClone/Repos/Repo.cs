using IMDBClone.Data;
using IMDBClone.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IMDBClone.Repos
{
    public class Repo<T> : IRepo<T> where T : class
    {
        public ApplicationDbContext _context { get; }

        public Repo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
