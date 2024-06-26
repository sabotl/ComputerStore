namespace ComputerStore.Application.UseCase
{
    public class UserUseCase
    {
        private readonly Domain.Services.IUserService _useCase;
        private readonly Mappers.UserMapper _mapper;
        public UserUseCase(Domain.Services.IUserService userService, Mappers.UserMapper mapper)
        {
            _useCase = userService;
            _mapper = mapper;
        }

        public async Task<(string access, string refresh)> Authorize(DTOs.LoginDTO loginDTO)
        {
            var tokens = await _useCase.Authorize(loginDTO.login, loginDTO.password);
            return tokens;
        }
        public async Task<DTOs.UserDTO> GetByID<T>(T id)
        {
            var user = await _useCase.GetByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException($"User with {id} not found");
            return _mapper.MapToDTO(user);

        }
        public async Task<string> UpdateAccessToken(string refreshToken)
        {
            return await _useCase.UpdateAccessToken(refreshToken);
        }
        public async Task UpdateProfile(DTOs.UserDTO userDTO)
        {
            var user = await _useCase.GetByIdAsync(userDTO.Id);
            if (user == null)
                throw new Exception("User is not found");

            await _useCase.UpdateAsync(user.id, _mapper.MapToEntity(userDTO));
        }
    }
}
