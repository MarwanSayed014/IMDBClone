using System.Linq.Expressions;

namespace IMDBClone.Repos.Interfaces
{
    public interface IRepo<T> where T : class
    {
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> FindAllAsync();
        public Task<int> SaveAsync();
    }
}
