using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IViewsService
    {

        Task<List<Views>> GetViewsAsync();
        Task<Views> GetViewsByIdAsync(int id);
        Task<List<Views>> GetViewsByArticleId(int articleId);
        Task<int> GetViewsCountByArticleIdAsync(int articleId);
        Task<Views> AddViewsAsync(Views views);
        Task<Views> UpdateViewsAsync(Views views);
        Task DeleteViewsAsync(int id);
    }
}
