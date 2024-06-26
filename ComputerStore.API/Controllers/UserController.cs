using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("profile"), Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var userIdClaim = claimsIdentity.FindFirst("user_id");
                    if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                    {
                        var userProfile = await _userUseCase.GetByID(userId);
                        if (userProfile != null)
                        {
                            return Ok(userProfile);
                        }
                        return NotFound("User profile not found.");
                    }
                    return BadRequest("Invalid user ID format or user ID not found in token.");
                }
                return Unauthorized("User identity not found.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProfile(Application.DTOs.UserDTO user)
        {
            try
            {
                await _userUseCase.UpdateProfile(user);
                return Ok();
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
