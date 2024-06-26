namespace ComputerStore.Application.Mappers
{
    public class UserMapper
    {
        public Domain.Entities.User MapToEntity(DTOs.UserDTO userDTO)
        {
            return new Domain.Entities.User
            {
                id = userDTO.Id,
                login = userDTO.Login,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email,
                DateCreated = userDTO.DateCreated,
                Role = userDTO.Role,
                IsAnonymous = userDTO.IsAnonymous
            };
        }
        public DTOs.UserDTO MapToDTO(Domain.Entities.User user)
        {
            return new DTOs.UserDTO
            {
                Id = user.id,
                Login = user.login,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DateCreated = user.DateCreated,
                Role = user.Role,
                IsAnonymous = user.IsAnonymous,
            };
        }
    }
}
