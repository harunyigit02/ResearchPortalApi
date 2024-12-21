using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchRequirementService
    {
        Task<List<ResearchRequirement>> GetResearchRequirementsAsync();
        Task<ResearchRequirement> GetResearchRequirementByIdAsync(int id);
        Task<Dictionary<string, object>> GetResearchRequirementByResearchIdAsync(int researchId);
        Task<List<Research>> GetMatchedResearchRequirementsAsync(ParticipantInfo participantInfo);
        Task<ResearchRequirement> AddResearchRequirementAsync(ResearchRequirement researchRequirement);
        Task<ResearchRequirement> UpdateResearchRequirementAsync(ResearchRequirement resreq);
        Task DeleteResearchRequirementAsync(int id);
    }
}
