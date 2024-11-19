using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetUserByUserName(string username);
        Task AddUser(User user);
    }
}
