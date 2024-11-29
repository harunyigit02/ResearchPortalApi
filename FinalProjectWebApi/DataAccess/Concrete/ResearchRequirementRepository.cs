using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ResearchRequirementRepository:IResearchRequirementRepository
    {

        private readonly ApplicationDbContext _context;

        public ResearchRequirementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResearchRequirement> AddAsync(ResearchRequirement researchRequirement)
        {
            await _context.ResearchRequirements.AddAsync(researchRequirement);
            await _context.SaveChangesAsync();
            return researchRequirement;
        }

        public async Task DeleteAsync(int id)
        {
            var researchRequirement = await _context.ResearchRequirements.FindAsync(id);
            if (researchRequirement != null)
            {
                _context.ResearchRequirements.Remove(researchRequirement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ResearchRequirement>> GetAllAsync()
        {
            return await _context.ResearchRequirements.ToListAsync();
        }

        public async Task<ResearchRequirement> GetByIdAsync(int id)
        {
            return await _context.ResearchRequirements.FindAsync(id);
        }

        public async Task<List<Research>> GetMatchingResearches(ParticipantInfo participant)
        {
            // Tüm araştırma şartlarını veritabanından çekiyoruz
            var researchRequirements =  _context.ResearchRequirements;

            // Katılımcı bilgileri ile eşleşen araştırmaları filtreliyoruz
            var matchingResearches =  await researchRequirements
                .Where(r =>
                    (r.MinAge <= participant.Age && r.MaxAge >= participant.Age) && // Yaş aralığı kontrolü
                    (!r.Gender.Any() || r.Gender.Contains(participant.Gender)) && // Cinsiyet kontrolü
                    (!r.Location.Any() || r.Location.Contains(participant.Location)) && // Lokasyon kontrolü
                    (!r.EducationLevel.Any() || r.EducationLevel.Contains(participant.EducationLevel)) && // Eğitim seviyesi kontrolü
                    (!r.Occupation.Any() || r.Occupation.Contains(participant.Occupation)) && // Meslek kontrolü
                    (!r.Ethnicity.Any() || r.Ethnicity.Contains(participant.Ethnicity)) && // Etnik köken kontrolü
                    (!r.MaritalStatus.Any() || r.MaritalStatus.Contains(participant.MaritalStatus)) && // Medeni hal kontrolü
                    (!r.ParentalStatus.Any() || r.ParentalStatus.Contains(participant.ParentalStatus)) && // Ebeveynlik durumu kontrolü
                    (!r.ChildStatus.Any() || r.ChildStatus.Contains(participant.ChildStatus)) && // Çocuk durumu kontrolü
                    (!r.DisabilityStatus.Any() || r.DisabilityStatus.Contains(participant.DisabilityStatus)) && // Engellilik durumu kontrolü
                    (!r.HousingType.Any() || r.HousingType.Contains(participant.HousingType)) // Konut türü kontrolü
                )
                .ToListAsync();

            return await researchRequirements.Select(r => r.Research).ToListAsync();
        }

        public Task UpdateAsync(ResearchRequirement researchRequirement)
        {
            throw new NotImplementedException();
        }
    }
}
