using System.Collections.Generic;
using System.Threading.Tasks;
using Bina.DAL.Data;
using Bina.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bina.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BinaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(BinaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}