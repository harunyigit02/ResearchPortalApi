using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IResearchRepository
    {

        Task<Research> GetByIdAsync(int id);
        Task<List<Research>> GetByUserIdAsync(int userId);
        Task<List<Research>> GetAllAsync();
        Task<List<Research>> GetCompletedAsync();

        Task<Research> AddAsync(Research research);
        Task UpdateAsync( Research research);
        Task DeleteAsync(int id);
    }
}

