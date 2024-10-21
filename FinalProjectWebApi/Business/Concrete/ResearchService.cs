using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
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

        public async Task<List<Research>> GetResearchesAsync()
        {
            return await _researchRepository.GetAllAsync();
        }

        public Task<Research> UpdateResearchAsync(Research research)
        {
            throw new NotImplementedException();
        }
    }
}
