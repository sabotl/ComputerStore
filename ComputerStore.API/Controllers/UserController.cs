using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ComputerStore.Application.UseCase.UserUseCase _userUseCase;
        public UserController(ComputerStore.Application.UseCase.UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return new JsonResult(1);   
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize(ComputerStore.Application.DTOs.LoginDTO loginDTO)
        {
            try
            {
                var tokens = await _userUseCase.Authorize(loginDTO);
                return Ok(new { access = tokens.access, refresh = tokens.refresh });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            try
            {
                var accessToken = await _userUseCase.UpdateAccessToken(refreshToken);
                return Ok(accessToken);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
