using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Repositories;
using ComputerStore.Domain.Services;

namespace ComputerStore.Application.Services
{
    public class UserService: BaseService<Domain.Entities.User>, Domain.Services.IUserService
    {
        private readonly Domain.Repositories.IUserRepostory _userRepostory;
        private readonly TokenService _tokenService;
        public UserService(Domain.Repositories.IUserRepostory userRepostory, TokenService tokenService): base(userRepostory) 
        {
            _userRepostory = userRepostory;
            _tokenService = tokenService;
        }

        public async Task<(string, string)> Authorize(string login, string password)
        {
            var user = await _userRepostory.GetByLogin(login);
            if (user == null || !PasswordHasher.VerifyPassword(password, user.password, user.salt))
            {
                throw new UnauthorizedAccessException("Invalid login or password");
            }
            return await _tokenService.CreateTokens(user.id);
        }
        public async Task<string> UpdateAccessToken(string refreshToken)
        {
            return await _tokenService.RefreshAccessToken(refreshToken);
        }
        public async Task<User?> GetByLogin(string login)
        {
            return await _userRepostory.GetByLogin(login);
        }
    }
}
