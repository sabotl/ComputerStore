namespace ComputerStore.Domain.Services
{
    public interface IBaseService<T>
    {
        Task<T?> GetByIdAsync<Type>(Type id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync<Type>(Type id, T entity);
        Task DeleteAsync(int entity);
        Task DeleteAsync(Guid entity);
    }
}
