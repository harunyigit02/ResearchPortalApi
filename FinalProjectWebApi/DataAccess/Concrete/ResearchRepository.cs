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

        public async Task<Research> GetByIdAsync(int id)
        {
            return await _context.Researches.FindAsync(id);
        }

        public Task UpdateAsync(Research research)
        {
            throw new NotImplementedException();
        }
    }
}