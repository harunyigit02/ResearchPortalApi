using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface ITemporaryUserRepository
    {
        Task<TemporaryUser> GetByIdAsync(int id);
        Task<List<TemporaryUser>> GetExpiredUsersAsync(DateTime expirationThreshold);
        Task<TemporaryUser> GetUserByUserName(string email);
        Task AddAsync(TemporaryUser temporaryUser);
        Task DeleteAsync(int id);
    }
}
