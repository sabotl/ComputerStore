using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;

namespace ComputerStore.Application.Services
{
    public class UserService: BaseService<Domain.Entities.User>, Domain.Services.IUserService
    {
        private readonly Domain.Repositories.IUserRepostory _userRepostory;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserService(Domain.Repositories.IUserRepostory userRepostory, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
            : base(userRepostory)
        {
            _userRepostory = userRepostory;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<(string, string)> Authorize(string login, string password)
        {
            var user = await _userRepostory.GetByLogin(login);
            if (user == null || !PasswordHasher.VerifyPassword(password, user.password, user.salt))
            {
                throw new UnauthorizedAccessException("Invalid login or password");
            }
            var accessToken = _accessTokenService.CreateAccessToken(user.id);
            var refreshToken = await _refreshTokenService.CreateRefreshToken(user.id);
            return (accessToken, refreshToken);
        }

        public async Task<string> UpdateAccessToken(string refreshToken)
        {
            return await _refreshTokenService.RefreshAccessToken(refreshToken);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _userRepostory.GetByLogin(login);
        }
    }
}
