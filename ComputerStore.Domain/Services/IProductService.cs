using System.Linq.Expressions;

namespace ComputerStore.Domain.Services
{
    public interface IProductService: IBaseService<Entities.Goods>
    {
        Task<IReadOnlyList<Entities.Goods>?> FindAsync(Expression<Func<Entities.Goods, bool>> predicate);
        Task<int> CountAsync();
    }
}
