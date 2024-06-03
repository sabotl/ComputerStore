namespace ComputerStore.Application.Services.Interfaces
{
    public interface IGoodsAppService<T> where T : class
    {
        Task CreateProduct(T dTO);
    }
}
