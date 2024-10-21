using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ViewsRepository
    {

        private readonly ApplicationDbContext _context;

        public ViewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Views> AddAsync(Views views)
        {
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

        public Task UpdateAsync(ResearchRequirement researchRequirement)
        {
            throw new NotImplementedException();
        }
    }
}
