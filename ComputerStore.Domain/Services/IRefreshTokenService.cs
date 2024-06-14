namespace ComputerStore.Domain.Services
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshToken(Guid userId);
        Task<string> RefreshAccessToken(string refreshToken);
    }
}
