using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IArticleService
    {

        Task<List<ArticleDto>> GetArticlesAsync();
        Task<PagingResult<ArticleDto>> GetArticlesPagedAsync(int pageNumber, int pageSize);
        Task<PagingResult<ArticleDto>> GetArticlesByUserIdAsync(int userId, int pageNumber, int pageSize);
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> AddArticleAsync(Article article);
        Task<Article>UpdateArticleAsync(Article article);
        Task<Article>DeleteArticleAsync(int id);

    }
}
