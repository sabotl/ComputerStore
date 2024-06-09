using System.Linq.Expressions;

namespace ComputerStore.Domain.Repositories
{
    public interface IProductRepository: IBaseRepository<Entities.Goods>
    {
        Task<IReadOnlyList<Entities.Goods>?> FindAsync(Expression<Func<Entities.Goods, bool>> predicate);
        Task<int> CountAsync();
    }
}
