using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAuthService
    {
        Task Register(string email, string password);
        Task<string> Login(string email, string password);
        Task VerifyEmail(string email, string verificationCode);
        Task<PagingResult<UserManageDto>> GetUsersAsync(int pageNumber, int pageSize, string? roleFilter, string? keyword, DateTime? minDate, DateTime? maxDate);
        Task UpdateUserRoleAsync(int userId, string newRole);
        Task<UserManageDto> GetUserByUserIdAsync(int userId);

    }
}