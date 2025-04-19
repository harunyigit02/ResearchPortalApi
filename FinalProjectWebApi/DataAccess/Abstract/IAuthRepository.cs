using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<PagingResult<UserManageDto>> GetUsersPagedAsync(int pageNumber, int pageSize, string? roleFilter, string? keyword, DateTime? minDate, DateTime? maxDate);
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByUserName(string username);
        Task AddUser(User user);
        Task UpdateAsync(User user);

    }
}
