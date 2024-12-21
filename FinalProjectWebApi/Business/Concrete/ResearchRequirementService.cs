using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
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
        public async Task<Dictionary<string, object>> GetResearchRequirementByResearchIdAsync(int researchId)
        {
            return await _researchRequirementRepository.GetByResearchId(researchId);
        }


        public async Task<List<ResearchRequirement>> GetResearchRequirementsAsync()
        {
            return await _researchRequirementRepository.GetAllAsync();
        }
        public async Task<List<Research>> GetMatchedResearchRequirementsAsync(ParticipantInfo participantInfo)
        {
            return await _researchRequirementRepository.GetMatchingResearches(participantInfo);
        }

        public Task<ResearchRequirement> UpdateResearchRequirementAsync(ResearchRequirement resreq)
        {
            throw new NotImplementedException();
        }
    }
}
