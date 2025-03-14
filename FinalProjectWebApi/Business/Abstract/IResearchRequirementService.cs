using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchRequirementService
    {
        Task<List<ResearchRequirement>> GetResearchRequirementsAsync();
        Task<ResearchRequirement> GetResearchRequirementByIdAsync(int id);
        Task<ResearchRequirement> GetResearchRequirementByResearchIdAsync(int researchId);
        Task<PagingResult<Research>> GetMatchedResearchRequirementsAsync(ParticipantInfo participant,
    int pageNumber,
    int pageSize,
    string? keyword,
    int? categoryId,
    DateTime? minDate,
    DateTime? maxDate);
        Task<ResearchRequirement> AddResearchRequirementAsync(ResearchRequirement researchRequirement);
        Task<ResearchRequirement> UpdateResearchRequirementAsync(int researchId, ResearchRequirement researchRequirement);
        Task DeleteResearchRequirementAsync(int id);
    }
}
