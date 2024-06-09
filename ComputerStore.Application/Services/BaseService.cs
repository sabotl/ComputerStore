
namespace ComputerStore.Application.Services
{
    public abstract class BaseService<T> : ComputerStore.Domain.Services.IBaseService<T> where T : class
    {
        protected readonly ComputerStore.Domain.Repositories.IBaseRepository<T> _repository;
        public BaseService(ComputerStore.Domain.Repositories.IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task DeleteAsync(Guid entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
        public async Task UpdateAsync(Guid id, T entity)
        {
            await _repository.UpdateAsync(id, entity);
        }
    }
}
