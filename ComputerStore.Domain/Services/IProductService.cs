using System.Linq.Expressions;

namespace ComputerStore.Domain.Services
{
    public interface IProductService<T>: IBaseService<T> where T : class
    {
        Task<IEnumerable<T>?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }
}
