using ComputerStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ComputerStore.Infrastructure.Repository
{
    public class BaseRepository<T> : ComputerStore.Domain.Repositories.IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.Set<T>().FindAsync(id);
            if(entry != null)
            {
                _context.Set<T>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var entiry = await _context.Set<T>().FindAsync(id);
            if (entiry != null)
            {
                _context.Set<T>().Remove(entiry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                return entity;
            return null;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                return entity;
            return null;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task AbstractionUpdateAsync<TId>(TId id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await AbstractionUpdateAsync(id, entity);
        }
        public async Task UpdateAsync(Guid id, T entity)
        {
            await AbstractionUpdateAsync(id, entity);
        }
    }
}
