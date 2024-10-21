using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ResearchRequirementService : IResearchRequirementService
    {

        private readonly ApplicationDbContext _context;

        public ResearchRequirementService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<ResearchRequirement> AddResearchRequirementAsync(ResearchRequirement resreq)
        {
            throw new NotImplementedException();
        }

        public Task DeleteResearchRequirementAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResearchRequirement> GetResearchRequirementByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResearchRequirement>> GetResearchRequirementsAsync()
        {
            return _context.ResearchRequirements.ToListAsync();
        }

        public Task<ResearchRequirement> UpdateResearchRequirementAsync(ResearchRequirement resreq)
        {
            throw new NotImplementedException();
        }
    }
}
