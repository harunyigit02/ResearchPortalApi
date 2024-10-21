using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ArticleService : IArticleService
    {

        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> AddArticleAsync(Article article)
        {
            return await _articleRepository.AddAsync(article);

        }

        public async Task<Article> DeleteArticleAsync(int id)
        {
            // 1. Öncelikle silinecek kaydın var olup olmadığını kontrol edelim
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                // Kayıt bulunamadıysa false döndürüyoruz (ya da özel bir hata fırlatabiliriz)
                return null;
            }

            // 2. Kayıt varsa silme işlemini gerçekleştiriyoruz
            await _articleRepository.DeleteAsync(id);

            // 3. İşlem başarılı ise true döndürüyoruz
            return article;
        }


        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _articleRepository.GetAllAsync();
        }

        public Task<Article> UpdateArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
