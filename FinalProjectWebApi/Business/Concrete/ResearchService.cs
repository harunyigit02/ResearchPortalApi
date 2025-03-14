using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.DataAccess.Concrete;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ResearchService : IResearchService
    {

        private readonly IResearchRepository _researchRepository;

        public ResearchService(IResearchRepository researchRepository)
        {
            _researchRepository = researchRepository;
        }


        public async Task<Research> AddResearchAsync(Research research)
        {
            research.PublishedAt = DateTime.UtcNow;
            research.IsCompleted = false;
            return await _researchRepository.AddAsync(research);
            
        }

        public async Task DeleteResearchAsync(int id)
        {
            await _researchRepository.DeleteAsync(id);
        }

        public async Task<Research> GetResearchByIdAsync(int id)
        {
            return await _researchRepository.GetByIdAsync(id);
        }
        public async Task<List<Research>> GetResearchesByUserIdAsync(int userId)
        {
            // Kullanıcıya ait makaleleri repository katmanından alıyoruz
            var researches = await _researchRepository.GetByUserIdAsync(userId);

            // İş mantığı ve dönüşüm işlemleri yapılabilir
            

            return researches;
        }

        public async Task<List<Research>> GetResearchesAsync()
        {
            return await _researchRepository.GetAllAsync();
        }
        public async Task<PagingResult<Research>> GetCompletedResearchesAsync(
              int pageNumber,
              int pageSize,
              int? categoryId,
              string? keyword,
              DateTime? minDate,
              DateTime? maxDate
            )
              
        {
            return await _researchRepository.GetCompletedAsync(
                pageNumber,
                pageSize,
                categoryId,
                keyword,
                minDate,
                maxDate);
        }

        public async Task<Research> UpdateResearchAsync(int id,Research research)
        {
            var existingResearch = await _researchRepository.GetByIdAsync(id);
            if (existingResearch == null)
            {
                throw new ArgumentNullException(nameof(existingResearch), "Research not found");
            }

            // Güncellenmiş özellikleri atama
            existingResearch.Title = research.Title;
            existingResearch.Description = research.Description;
            existingResearch.CategoryId = research.CategoryId;
            existingResearch.IsFaceToFace = research.IsFaceToFace;


            // Repository üzerinden güncelleme işlemini yap
            await _researchRepository.UpdateAsync(existingResearch);
            return existingResearch;

        }

        public async Task<PagingResult<Research>> GetPagedResearchesAsync(int pageNumber,int pageSize)
        {
            var result = await _researchRepository.GetResearchesPagedAsync(pageNumber, pageSize);
            var researches = result.Items;

            return new PagingResult<Research>
            {
                Items = researches,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize

            };
        }
        public async Task<PagingResult<Research>> GetPagedResearhesByUserIdAsync(
            int userId,
            int pageNumber,
            int pageSize,
            string? keyword,
            int? categoryId,
            DateTime? minDate,
            DateTime? maxDate
            
            )
        {
            var result = await _researchRepository.GetPagedResearchesByUserIdAsync(userId,pageNumber, pageSize,keyword,categoryId,minDate,maxDate);
            

            return result;

        }
    }
}
