using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IOptionRepository
    {
        Task<Option> GetByIdAsync(int id);
        Task<List<Option>> GetAllAsync();
        Task<Option> AddAsync(Option option);
        Task UpdateAsync(Option option);
        Task DeleteAsync(int id);
    }
}
