namespace ComputerStore.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task UpdateAsync(Guid id, T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(Guid id);
    }
}
