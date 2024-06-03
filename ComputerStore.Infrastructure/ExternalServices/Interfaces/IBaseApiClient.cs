namespace ComputerStore.Infrastructure.ExternalServices.Interfaces
{
    public interface IBaseApiClient<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>?> GetAll();
        Task<T?> GetByNameAsync(string name);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(string id);
    }
}
