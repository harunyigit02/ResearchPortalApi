using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IResearchRequirementRepository
    {
        Task<ResearchRequirement> AddAsync(ResearchRequirement researchRequirement);
        Task<List<Research>> GetMatchingResearches(ParticipantInfo participant);
        Task DeleteAsync(int id);
        Task<List<ResearchRequirement>> GetAllAsync();
        Task<ResearchRequirement> GetByIdAsync(int id);

        Task UpdateAsync(ResearchRequirement researchRequirement);

    }
}
