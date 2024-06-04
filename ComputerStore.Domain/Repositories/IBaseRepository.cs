namespace ComputerStore.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync<Tid>(Tid id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync<Tid>(Tid id, T entity);
        Task DeleteAsync<Tid>(Tid id);
    }
}
