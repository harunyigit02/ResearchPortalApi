using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Mappings;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<PagingResult<ArticleDto>> GetArticlesByUserIdAsync(int userId,int pageNumber,int pageSize,int? categoryId,string? keyword)
        {
            // Kullanıcıya ait makaleleri repository katmanından alıyoruz
            var result = await _articleRepository.GetPagedArticlesByUserIdAsync(userId, pageNumber, pageSize,categoryId,keyword);

            // İş mantığı ve dönüşüm işlemleri yapılabilir
            var articleDtos = result.Items.Select(article => _mapper.MapToDto(article)).ToList();

            return new PagingResult<ArticleDto>
            {
                Items = articleDtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public Task<Article> UpdateArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }
        public async Task<PagingResult<ArticleDto>> GetArticlesPagedAsync(int pageNumber, int pageSize,int? categoryId,string? keyword)
        {
            var result = await _articleRepository.GetArticlesPagedAsync(pageNumber, pageSize,categoryId,keyword);

            // Article'den ArticleDto'ya dönüşüm işlemi
            var articleDtos = result.Items.Select(article => _mapper.MapToDto(article)).ToList();

            return new PagingResult<ArticleDto>
            {
                Items = articleDtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}
