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
            return await _context.ResearchRequirements.Include(r=>r.Research).ToListAsync();
        }

        public async Task<ResearchRequirement> GetByIdAsync(int id)
        {
            return await _context.ResearchRequirements.FindAsync(id);
        }
        public async Task<Dictionary<string, object>> GetByResearchId(int researchId)
        {
            var requirement = await _context.ResearchRequirements
        .FirstOrDefaultAsync(rr => rr.ResearchId == researchId);

            if (requirement == null)
                return null;

            // Tüm özellikleri kontrol ederek sadece null olmayanları ekliyoruz
            var result = requirement.GetType()
                .GetProperties() // Modelin tüm özelliklerini al
                .Where(prop => prop.GetValue(requirement) != null) // Null olmayanları filtrele
                .ToDictionary(
                    prop => prop.Name, // Özellik adını al
                    prop => prop.GetValue(requirement) // Özellik değerini al
                );

            return result;

        }


        public async Task<List<Research>> GetMatchingResearches(ParticipantInfo participant)
        {
            // Katılımcı bilgileri ile eşleşen araştırmaları filtreliyoruz
            var matchingResearches = await _context.ResearchRequirements
                .Where(r =>
                    ( r.MinAge== null || r.MinAge <= participant.Age && r.MaxAge== null || r.MaxAge >= participant.Age) &&
                    // Gender listesinde katılımcının cinsiyetinin olup olmadığını kontrol ediyoruz
                    r.Gender == null || r.Gender.Contains(participant.Gender) &&
                    // Location listesinde katılımcının lokasyonunun olup olmadığını kontrol ediyoruz
                    r.Location== null || r.Location.Contains(participant.Location) &&
                    // EducationLevel listesinde katılımcının eğitim seviyesinin olup olmadığını kontrol ediyoruz
                    r.EducationLevel== null || r.EducationLevel.Contains(participant.EducationLevel) &&
                    // Occupation listesinde katılımcının mesleğinin olup olmadığını kontrol ediyoruz
                    r.Occupation== null || r.Occupation.Contains(participant.Occupation) &&
                    // Ethnicity listesinde katılımcının etnik kimliğinin olup olmadığını kontrol ediyoruz
                    r.Ethnicity == null || r.Ethnicity.Contains(participant.Ethnicity) &&
                    // MaritalStatus listesinde katılımcının medeni durumunun olup olmadığını kontrol ediyoruz
                    r.MaritalStatus == null || r.MaritalStatus.Contains(participant.MaritalStatus) &&
                    // ParentalStatus listesinde katılımcının ebeveynlik durumunun olup olmadığını kontrol ediyoruz
                    r.ParentalStatus == null || r.ParentalStatus.Contains(participant.ParentalStatus) &&
                    // ChildStatus listesinde katılımcının çocuk durumu olup olmadığını kontrol ediyoruz
                     r.ChildStatus == null || r.ChildStatus.Contains(participant.ChildStatus) &&
                    // DisabilityStatus listesinde katılımcının engellilik durumunun olup olmadığını kontrol ediyoruz
                    r.DisabilityStatus == null || r.DisabilityStatus.Contains(participant.DisabilityStatus) &&
                    // HousingType listesinde katılımcının konut türünün olup olmadığını kontrol ediyoruz
                    r.HousingType == null || r.HousingType.Contains(participant.HousingType)
                )
                // İlişkili Research tablosunu yükle
                .Include(r => r.Research)
                .Select(r => r.Research)
                .ToListAsync();

            return matchingResearches;
        }


        public Task UpdateAsync(ResearchRequirement researchRequirement)
        {
            throw new NotImplementedException();
        }
    }
}
