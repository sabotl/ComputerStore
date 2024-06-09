using ComputerStore.Domain.Entities;

namespace ComputerStore.Domain.Repositories
{
    public interface IUserRepostory: IBaseRepository<Domain.Entities.User>
    {
        Task<Domain.Entities.User?> GetByLogin(string login);
        Task SaveRefreshToken(Guid id, string refreshToken, DateTime dateTime);
        Task DeleteRefreshToken(Guid id);
        Task<Entities.RefreshToken?> GetCurrencyRefreshToken(string refreshToken);
    }
}
