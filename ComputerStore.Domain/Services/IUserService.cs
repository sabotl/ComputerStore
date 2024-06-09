namespace ComputerStore.Domain.Services
{
    public interface IUserService: IBaseService<Domain.Entities.User>
    {
        Task<Domain.Entities.User?> GetByLogin(string login);
        Task<(string, string)> Authorize(string login , string password);
        Task<string> UpdateAccessToken(string refreshToken);
    }
}
