using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                 await _authService.Register(registerDto.Email, registerDto.Password);
                return Ok(registerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var token = await _authService.Login(loginDto.Email, loginDto.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users= await _authService.GetUsersAsync();
            return Ok(users);
        }
    }
}
