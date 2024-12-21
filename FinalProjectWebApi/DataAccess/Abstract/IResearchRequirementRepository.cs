using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IResearchRequirementRepository
    {
        Task<ResearchRequirement> AddAsync(ResearchRequirement researchRequirement);
        Task DeleteAsync(int id);
        Task<List<ResearchRequirement>> GetAllAsync();
        Task<ResearchRequirement> GetByIdAsync(int id);
        Task<Dictionary<string, object>> GetByResearchId(int researchId);
        Task<List<Research>> GetMatchingResearches(ParticipantInfo participant);
        Task UpdateAsync(ResearchRequirement researchRequirement);
    }
}
