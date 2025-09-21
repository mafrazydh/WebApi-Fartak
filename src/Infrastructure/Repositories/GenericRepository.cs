using Application.Contracts;
using Domin.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var guid = new Guid(id);
            return await _dbSet.FindAsync(guid);
             
            
        }

        public async Task<T> AddAsync(T entity)
        {
            var Entity = await _dbSet.AddAsync(entity);
            return Entity.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var newEntity = _dbSet.Update(entity);
            return newEntity.Entity;
        }

        public async Task Delete(string id)
        {
            var guid = new Guid(id);
            var entity = _dbSet.Find(guid);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

       
    }
}
