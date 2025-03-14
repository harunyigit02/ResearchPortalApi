using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ArticleRepository : IArticleRepository
    {

        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> AddAsync(Article article)
        {
            
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> DeleteArticlesAsync(List<int> articleIds)
        {
            // Silinecek makaleleri veritabanından alıyoruz
            var articlesToDelete = await _context.Articles
                .Where(a => articleIds.Contains(a.Id))
                .ToListAsync();

            if (articlesToDelete.Count == 0)
            {
                return false; // Eğer silinecek makale yoksa false döner
            }

            // Toplu silme işlemi
            _context.Articles.RemoveRange(articlesToDelete);

            try
            {
                // Değişiklikleri veritabanına kaydediyoruz
                await _context.SaveChangesAsync();
                return true; // Silme başarılı oldu
            }
            catch (Exception)
            {
                // Hata durumunda true dönmez
                return false;
            }
        }

        public async Task<IQueryable<Article>> GetAllAsync()
        {
            return _context.Articles.AsQueryable();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
        public async Task<PagingResult<Article>> GetPagedArticlesByUserIdAsync(int userId,int pageNumber,int pageSize,int? categoryId,string? keyword,DateTime? minDate,DateTime? maxDate)
        {
            var query = _context.Articles
               .Where(a => a.PublishedBy == userId);
            if (categoryId.HasValue)
            {
                query=query.Where(a=>a.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                var loweredKeyword = keyword.ToLower();
                query = query.Where(r => r.Title.ToLower().Contains(loweredKeyword) || r.Description.Contains(loweredKeyword));
            }
            if (minDate.HasValue)
            {
                minDate = DateTime.SpecifyKind(minDate.Value, DateTimeKind.Utc);
                query = query.Where(r => r.PublishedAt > minDate.Value);
            }
            if (maxDate.HasValue)
            {
                maxDate = DateTime.SpecifyKind(maxDate.Value, DateTimeKind.Utc);
                query = query.Where(r => r.PublishedAt < maxDate.Value);
            }

            int totalCount = await query.CountAsync();
            var articles = await query
                .Skip((pageNumber - 1) * pageSize) // Sayfalama için atlama
                .Take(pageSize) // Sayfa boyutunda veri çekme
                .ToListAsync(); // Listeleme
            return new PagingResult<Article>
            {
                Items = articles,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task UpdateAsync( Article article)
        {
            
        }

        public async Task<PagingResult<Article>> GetArticlesPagedAsync(int pageNumber, int pageSize,int? categoryId,string? keyword,DateTime? minDate, DateTime? maxDate)
        {
            var query = _context.Articles.AsQueryable();

            if (categoryId.HasValue)
            {
                query=query.Where(r => r.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                var loweredKeyword = keyword.ToLower();
                query = query.Where(r=>r.Title.ToLower().Contains(loweredKeyword) || r.Description.Contains(loweredKeyword));
            }
            if (minDate.HasValue)
            {
                minDate = DateTime.SpecifyKind(minDate.Value, DateTimeKind.Utc);
                query = query.Where(r => r.PublishedAt > minDate.Value);
            }
            if (maxDate.HasValue)
            {
                maxDate = DateTime.SpecifyKind(maxDate.Value, DateTimeKind.Utc);
                query = query.Where(r => r.PublishedAt < maxDate.Value);
            }

            var totalItems = await query.CountAsync();

            var articles = await query
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Article>
            {
                Items = articles,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


       
    }
}
