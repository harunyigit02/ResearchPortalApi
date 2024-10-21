using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IArticleService
    {

        Task<List<Article>> GetArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> AddArticleAsync(Article article);
        Task<Article>UpdateArticleAsync(Article article);
        Task<Article>DeleteArticleAsync(int id);

    }
}
