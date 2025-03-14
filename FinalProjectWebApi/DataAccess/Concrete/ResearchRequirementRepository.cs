using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
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
        public async Task<ResearchRequirement> GetByResearchId(int researchId)
        {
            var requirement = await _context.ResearchRequirements

        .FirstOrDefaultAsync(rr => rr.ResearchId == researchId);

            return requirement;
                

            // Tüm özellikleri kontrol ederek sadece null olmayanları ekliyoruz
            

            

        }

       


        public async Task<PagingResult<Research>> GetMatchingResearchesAsync(
    ParticipantInfo participant,
    int pageNumber,
    int pageSize,
    string? keyword,
    int? categoryId,
    DateTime? minDate,
    DateTime? maxDate)
        {
            var queryable = _context.ResearchRequirements
                .Where(r =>
                    (r.MinAge == null || r.MinAge <= participant.Age) &&
                    (r.MaxAge == null || r.MaxAge >= participant.Age) &&
                    (r.Gender == null || r.Gender.Contains(participant.Gender)) &&
                    (r.Location == null || r.Location.Contains(participant.Location)) &&
                    (r.EducationLevel == null || r.EducationLevel.Contains(participant.EducationLevel)) &&
                    (r.Occupation == null || r.Occupation.Contains(participant.Occupation)) &&
                    (r.Ethnicity == null || r.Ethnicity.Contains(participant.Ethnicity)) &&
                    (r.MaritalStatus == null || r.MaritalStatus.Contains(participant.MaritalStatus)) &&
                    (r.ParentalStatus == null || r.ParentalStatus.Contains(participant.ParentalStatus)) &&
                    (r.ChildStatus == null || r.ChildStatus.Contains(participant.ChildStatus)) &&
                    (r.DisabilityStatus == null || r.DisabilityStatus.Contains(participant.DisabilityStatus)) &&
                    (r.HousingType == null || r.HousingType.Contains(participant.HousingType)) &&
                    r.Research.IsCompleted == true
                )
                // İlişkili Research tablosunu yükle
                .Include(r => r.Research)
                .ThenInclude(r => r.Questions)
                .ThenInclude(q => q.Options)
                .Select(r => r.Research);

            // Arama işlemi
            if (!string.IsNullOrEmpty(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                queryable = queryable.Where(r => r.Title.ToLower().Contains(lowerKeyword) || r.Description.ToLower().Contains(lowerKeyword));
            }

            // Kategori filtrelemesi
            if (categoryId.HasValue)
            {
                queryable = queryable.Where(r => r.CategoryId == categoryId.Value);
            }
            if (minDate.HasValue)
            {
                minDate = DateTime.SpecifyKind(minDate.Value, DateTimeKind.Utc);
                queryable = queryable.Where(r=>r.PublishedAt>minDate.Value);
            }
            if (maxDate.HasValue)
            {
                maxDate = DateTime.SpecifyKind(maxDate.Value, DateTimeKind.Utc);
                queryable = queryable.Where(r => r.PublishedAt < maxDate.Value);
            }
            

            // Sayfalama işlemi
            var totalItems = await queryable.CountAsync();
            var researches = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Research>
            {
                Items = researches,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task UpdateAsync(ResearchRequirement researchRequirement)
        {

            _context.ResearchRequirements.Update(researchRequirement);
            await _context.SaveChangesAsync();
        }
    }
}
