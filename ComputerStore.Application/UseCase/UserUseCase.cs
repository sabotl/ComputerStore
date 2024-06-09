namespace ComputerStore.Application.UseCase
{
    public class UserUseCase
    {
        private readonly Domain.Services.IUserService _useCase;

        public UserUseCase(Domain.Services.IUserService userService)
        {
            _useCase = userService;
        }

        public async Task<(string access, string refresh)> Authorize(DTOs.LoginDTO loginDTO)
        {
            var tokens = await _useCase.Authorize(loginDTO.login, loginDTO.password);
            return tokens;
        }
        public async Task<string> UpdateAccessToken(string refreshToken)
        {
            return await _useCase.UpdateAccessToken(refreshToken);
        }
    }
}
