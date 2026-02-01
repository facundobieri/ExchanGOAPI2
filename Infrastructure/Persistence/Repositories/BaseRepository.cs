using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ExchanGODbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ExchanGODbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public virtual async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public virtual void Update(T entity) =>
            _dbSet.Update(entity);

        public virtual void Delete(T entity) =>
            _dbSet.Remove(entity);

        public virtual Task SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}