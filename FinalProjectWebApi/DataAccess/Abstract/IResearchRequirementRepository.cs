using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IResearchRequirementRepository
    {
        Task<ResearchRequirement> AddAsync(ResearchRequirement researchRequirement);
        Task DeleteAsync(int id);
        Task<List<ResearchRequirement>> GetAllAsync();
        Task<ResearchRequirement> GetByIdAsync(int id);
        Task<ResearchRequirement> GetByResearchId(int researchId);
        Task<PagingResult<Research>> GetMatchingResearchesAsync(
    ParticipantInfo participant,
    int pageNumber,
    int pageSize,
    string? keyword,
    int? categoryId,
    DateTime? minDate,
    DateTime? maxDate);
        Task UpdateAsync(ResearchRequirement researchRequirement);
    }
}
