using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IViewsRepository
    {
        Task<Views> GetByIdAsync(int id);
        Task<List<Views>> GetAllAsync();
        Task<Views> AddAsync(Views views);
        Task UpdateAsync(Views views);
        Task DeleteAsync(int id);
        Task<List<Views>> GetByArticleIdAsync(int articleId);
        Task<int> GetViewsCountByArticleIdAsync(int articleId);
    }
}
