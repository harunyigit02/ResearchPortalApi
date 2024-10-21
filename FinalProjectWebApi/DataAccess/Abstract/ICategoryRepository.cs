using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface ICategoryRepository
    {

        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
