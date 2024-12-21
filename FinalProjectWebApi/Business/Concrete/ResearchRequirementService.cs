using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ResearchRequirementService : IResearchRequirementService
    {

        private readonly IResearchRequirementRepository _researchRequirementRepository;

        public ResearchRequirementService(IResearchRequirementRepository researchRequirementRepository)
        {
            _researchRequirementRepository = researchRequirementRepository;
        }
        public async Task<ResearchRequirement> AddResearchRequirementAsync(ResearchRequirement researchRequirement)
        {
            return await _researchRequirementRepository.AddAsync(researchRequirement);
        }

        public Task DeleteResearchRequirementAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResearchRequirement> GetResearchRequirementByIdAsync(int id)
        {
            return await _researchRequirementRepository.GetByIdAsync(id);
        }
        public async Task<ResearchRequirement> GetResearchRequirementByResearchIdAsync(int researchId)
        {
            return await _researchRequirementRepository.GetByResearchId(researchId);
        }


        public async Task<List<ResearchRequirement>> GetResearchRequirementsAsync()
        {
            return await _researchRequirementRepository.GetAllAsync();
        }
        public async Task<PagingResult<Research>> GetMatchedResearchRequirementsAsync(ParticipantInfo participant,
    int pageNumber,
    int pageSize,
    string? keyword,
    int? categoryId,
    DateTime? minDate,
    DateTime? maxDate)
        {
            return await _researchRequirementRepository.GetMatchingResearchesAsync(participant, pageNumber, pageSize, keyword,categoryId,minDate,maxDate);
        }

        public Task<ResearchRequirement> UpdateResearchRequirementAsync(ResearchRequirement resreq)
        {
            throw new NotImplementedException();
        }
    }
}
