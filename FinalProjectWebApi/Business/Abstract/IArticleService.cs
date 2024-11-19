using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IArticleService
    {

        Task<List<ArticleDto>> GetArticlesAsync();
        Task<List<ArticleDto>> GetArticlesByUserIdAsync(int userId);
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> AddArticleAsync(Article article);
        Task<Article>UpdateArticleAsync(Article article);
        Task<Article>DeleteArticleAsync(int id);

    }
}
