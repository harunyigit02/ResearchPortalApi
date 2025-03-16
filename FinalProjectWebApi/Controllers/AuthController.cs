using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto verifyEmailDto)
        {
            try
            {
                await _authService.VerifyEmail(verifyEmailDto.Email, verifyEmailDto.VerificationCode);
                return Ok(new { message = "Email doğrulandı ve kullanıcı kaydedildi.", email = verifyEmailDto.Email });
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
        public async Task<IActionResult> GetUsers(int pageNumber, int pageSize, string? roleFilter, string? keyword)
        {
            var users= await _authService.GetUsersAsync(pageNumber, pageSize, roleFilter, keyword);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserRole/{userId}")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] RoleUpdateRequest request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value; // JWT token'dan kullanıcının rolünü alıyoruz
            if (userRole != "Admin")
            {
                return Unauthorized("Bu işlem için admin yetkisine sahip olmanız gerekiyor");
            }

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); // JWT'den geçerli userId alıyoruz
            if (currentUserId == userId) // Eğer admin kendi rolünü değiştirmeye çalışıyorsa
            {
                return BadRequest("Admin kendi rolünü değiştiremez");
            }

            try
            {
                // Kullanıcının rolünü admin değiştirebilir
                await _authService.UpdateUserRoleAsync(userId, request.NewRole);
                return Ok("Kullanıcı rolü başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }
        }
    }
}
