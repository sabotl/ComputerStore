namespace ComputerStore.Domain.Services
{
    public interface IBaseService<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int entity);
        Task DeleteAsync(Guid entity);
    }
}
