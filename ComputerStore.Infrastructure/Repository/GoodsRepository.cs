using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ComputerStore.Infrastructure.Repository
{
    public class GoodsRepository : BaseRepository<Goods>, IProductRepository<Goods>
    {
        public GoodsRepository(Data.AppDbContext context):  base(context) 
        { 
        
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Goods>().CountAsync();
        }

        public async Task<IEnumerable<Goods>?> FindAsync(Expression<Func<Goods, bool>> predicate)
        {
            return await _context.Set<Goods>().Where(predicate).ToListAsync();
        }
    }
}
