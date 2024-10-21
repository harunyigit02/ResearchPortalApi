using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ResearchRequirementRepository
    {

        private readonly ApplicationDbContext _context;

        public ResearchRequirementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResearchRequirement> AddAsync(ResearchRequirement researchRequirement)
        {
            await _context.ResearchRequirements.AddAsync(researchRequirement);
            await _context.SaveChangesAsync();
            return researchRequirement;
        }

        public async Task DeleteAsync(int id)
        {
            var researchRequirement = await _context.ResearchRequirements.FindAsync(id);
            if (researchRequirement != null)
            {
                _context.ResearchRequirements.Remove(researchRequirement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ResearchRequirement>> GetAllAsync()
        {
            return await _context.ResearchRequirements.ToListAsync();
        }

        public async Task<ResearchRequirement> GetByIdAsync(int id)
        {
            return await _context.ResearchRequirements.FindAsync(id);
        }

        public Task UpdateAsync(ResearchRequirement researchRequirement)
        {
            throw new NotImplementedException();
        }
    }
}
