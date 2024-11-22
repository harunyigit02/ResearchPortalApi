using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAuthService
    {
        Task Register(string email, string password);
        Task<string> Login(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task UpdateUserRoleAsync(int userId, string newRole);
    }
}
