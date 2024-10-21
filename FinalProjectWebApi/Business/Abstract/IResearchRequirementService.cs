using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchRequirementService
    {
        Task<List<ResearchRequirement>> GetResearchRequirementsAsync();
        Task<ResearchRequirement> GetResearchRequirementByIdAsync(int id);
        Task<ResearchRequirement> AddResearchRequirementAsync(ResearchRequirement resreq);
        Task<ResearchRequirement> UpdateResearchRequirementAsync(ResearchRequirement resreq);
        Task DeleteResearchRequirementAsync(int id);
    }
}
