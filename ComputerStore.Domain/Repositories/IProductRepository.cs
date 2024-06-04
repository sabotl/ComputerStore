using System.Linq.Expressions;

namespace ComputerStore.Domain.Repositories
{
    public interface IProductRepository<T>: IBaseRepository<T> where T : class
    {
        Task<IReadOnlyList<T>?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }
}
