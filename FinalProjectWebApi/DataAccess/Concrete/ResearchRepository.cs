using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ResearchRepository : IResearchRepository
    {

        private readonly ApplicationDbContext _context;

        public ResearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Research> AddAsync(Research research)
        {
            await _context.Researches.AddAsync(research);
            await _context.SaveChangesAsync();
            return research;
        }

        public async Task DeleteAsync(int id)
        {
            var research = await _context.Researches.FindAsync(id);
            if (research != null)
            {
                _context.Researches.Remove(research);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Research>> GetAllAsync()
        {
            return await _context.Researches
                .Include(r=>r.Questions)
                .ThenInclude(q=>q.Options)
                .ToListAsync();
        }
        public async Task<List<Research>> GetCompletedAsync()
        {
            return await _context.Researches
                .Include(r => r.Questions)
                .ThenInclude(q => q.Options)
                .Where(r=>r.IsCompleted)
                .ToListAsync();
        }

        public async Task<Research> GetByIdAsync(int id)
        {
            return await _context.Researches.Include(r=>r.Questions).ThenInclude(q=>q.Options).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Research>> GetByUserIdAsync(int userId)
        {
            return _context.Researches
                .Where(a => a.PublishedBy == userId).ToList();  // Veritabanında filtreleme
        }

        public async Task UpdateAsync(Research research)
        {
            _context.Researches.Update(research);
            await _context.SaveChangesAsync();
            



        }
    }
}