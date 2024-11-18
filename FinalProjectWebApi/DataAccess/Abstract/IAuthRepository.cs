using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUserName(string username);
        Task AddUser(User user);
    }
}
