using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ViewsRepository:IViewsRepository
    {

        private readonly ApplicationDbContext _context;

        public ViewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Views> AddAsync(Views views)
        {
            views.ViewedAt = DateTime.UtcNow;
            await _context.Views.AddAsync(views);
            await _context.SaveChangesAsync();
            return views;
        }

        public async Task DeleteAsync(int id)
        {
            var views = await _context.Views.FindAsync(id);
            if (views != null)
            {
                _context.Views.Remove(views);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Views>> GetAllAsync()
        {
            return await _context.Views.ToListAsync();
        }

        public async Task<Views> GetByIdAsync(int id)
        {
            return await _context.Views.FindAsync(id);
        }

        public Task UpdateAsync(Views views)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Views>> GetByArticleIdAsync(int articleId)
        {
            return await _context.Views
                .Where(v => v.ViewedArticle == articleId)
                .ToListAsync();
        }
        public async Task<int> GetViewsCountByArticleIdAsync(int articleId)
        {
            return await _context.Views
                .CountAsync(v => v.ViewedArticle == articleId);
        }
    }
}
