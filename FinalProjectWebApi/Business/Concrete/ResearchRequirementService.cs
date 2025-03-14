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

        public async Task<ResearchRequirement> UpdateResearchRequirementAsync(int researchId,ResearchRequirement researchRequirement)
        {
            // İş kuralı ve atamalar burada yapılır
            if (researchRequirement.MinAge < 0 || researchRequirement.MaxAge < 0)
            {
                throw new ArgumentException("Age cannot be negative");
            }

            // MinAge, MaxAge'nin altında olmamalıdır (örnek bir iş kuralı)
            if (researchRequirement.MinAge > researchRequirement.MaxAge)
            {
                throw new ArgumentException("MinAge cannot be greater than MaxAge");
            }

            // Veritabanında mevcut kaydı alalım
            var existingRequirement = await _researchRequirementRepository.GetByResearchId(researchId);

            if (existingRequirement == null)
            {
                throw new ArgumentException("ResearchRequirement not found");
            }

            // Atamaları burada yapıyoruz
            
            existingRequirement.MinAge = researchRequirement.MinAge;
            existingRequirement.MaxAge = researchRequirement.MaxAge;
            existingRequirement.Gender = researchRequirement.Gender;
            existingRequirement.Location = researchRequirement.Location;
            existingRequirement.EducationLevel = researchRequirement.EducationLevel;
            existingRequirement.Occupation = researchRequirement.Occupation;
            existingRequirement.Ethnicity = researchRequirement.Ethnicity;
            existingRequirement.MaritalStatus = researchRequirement.MaritalStatus;
            existingRequirement.ParentalStatus = researchRequirement.ParentalStatus;
            existingRequirement.ChildStatus = researchRequirement.ChildStatus;
            existingRequirement.DisabilityStatus = researchRequirement.DisabilityStatus;
            existingRequirement.HousingType = researchRequirement.HousingType;

            // Güncelleme işlemi repository katmanına yapılır
            await _researchRequirementRepository.UpdateAsync(existingRequirement);

            return existingRequirement;
        }
    }
}
