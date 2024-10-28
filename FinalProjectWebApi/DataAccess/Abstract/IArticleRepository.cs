using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IArticleRepository
    {
        Task<Article> GetByIdAsync(int id);
        Task<IQueryable<Article>> GetAllAsync();
        Task<Article> AddAsync(Article article);
        Task UpdateAsync( Article article);
        Task DeleteAsync(int id);
    }
}
