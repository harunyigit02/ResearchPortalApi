using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IArticleRepository
    {
        Task<Article> GetByIdAsync(int id);
        Task<IQueryable<Article>> GetAllAsync();
        Task<PagingResult<Article>> GetArticlesPagedAsync(int pageNumber, int pageSize, int? categoryId, string? keyword);
        Task<PagingResult<Article>> GetPagedArticlesByUserIdAsync(int userId, int pageNumber, int pageSize, int? categoryId, string? keyword);
        Task<Article> AddAsync(Article article);
        Task UpdateAsync( Article article);
        Task DeleteAsync(int id);
    }
}
