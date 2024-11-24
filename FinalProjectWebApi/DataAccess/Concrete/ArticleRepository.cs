using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IQueryable<Article>> GetAllAsync()
        {
            return _context.Articles.AsQueryable();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
        public async Task<PagingResult<Article>> GetPagedArticlesByUserIdAsync(int userId,int pageNumber,int pageSize,int? categoryId)
        {
            var query = _context.Articles
               .Where(a => a.PublishedBy == userId);
            if (categoryId.HasValue)
            {
                query=query.Where(a=>a.CategoryId == categoryId.Value);
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

        public async Task<PagingResult<Article>> GetArticlesPagedAsync(int pageNumber, int pageSize,int? categoryId)
        {
            var query = _context.Articles.AsQueryable();

            if (categoryId.HasValue)
            {
                query=query.Where(r => r.CategoryId == categoryId.Value);
            }

            var totalItems = await query.CountAsync();

            var articles = await query
                .Skip((pageNumber - 1) * pageSize)  // Atlanacak öğe sayısı
                .Take(pageSize)                    // Alınacak öğe sayısı
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
