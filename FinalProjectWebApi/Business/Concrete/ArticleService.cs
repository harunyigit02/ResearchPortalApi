using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Mappings;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ArticleService : IArticleService
    {

        private readonly IArticleRepository _articleRepository;
        private readonly ArticleMapper _mapper;

        public ArticleService(IArticleRepository articleRepository,ArticleMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<Article> AddArticleAsync(Article article)
        {
            article.TotalViews = 0; // Varsayılan değer 0 olarak ayarlanıyor
            article.PublishedAt = DateTime.UtcNow;

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

        public async Task<List<ArticleDto>> GetArticlesAsync()
        {
            var articles = await _articleRepository.GetAllAsync();
            return await articles.Select(article => _mapper.MapToDto(article)).ToListAsync();
        }
        public async Task<List<ArticleDto>> GetArticlesByUserIdAsync(int userId)
        {
            // Kullanıcıya ait makaleleri repository katmanından alıyoruz
            var articles = await _articleRepository.GetArticlesByUserIdAsync(userId);

            // İş mantığı ve dönüşüm işlemleri yapılabilir
            var articleDtos = articles.Select(article => _mapper.MapToDto(article)).ToList();

            return articleDtos;
        }

        public Task<Article> UpdateArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
