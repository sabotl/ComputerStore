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

        public async Task DeleteAsync<Tid>(Tid id)
        {
            var entry = await _context.Set<T>().FindAsync(id);
            if(entry != null)
            {
                _context.Set<T>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync<Tid>(Tid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                return entity;
            return null;
        }

        private async Task AbstractionUpdateAsync<TId>(TId id, T entity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            var entityType = _context.Model.FindEntityType(typeof(T));
            var propertiesExceptKey = entityType.GetProperties().Where(p => !p.IsPrimaryKey() && !p.IsForeignKey());

            foreach (var property in propertiesExceptKey)
            {
                var propertyName = property.Name;
                var propertyValue = entityType.FindProperty(propertyName)?.GetGetter().GetClrValue(entity);
                if (propertyValue != null)
                {
                    _context.Entry(existingEntity).Property(propertyName).CurrentValue = propertyValue;
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync<Tid>(Tid id, T entity)
        {
            await AbstractionUpdateAsync(id, entity);
        }
    }
}
