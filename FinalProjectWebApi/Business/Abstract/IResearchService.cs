using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchService
    {

        Task<List<Research>> GetResearchesAsync();
        Task<Research> GetResearchByIdAsync(int id);
        Task<Research> AddResearchAsync(Research research);
        Task<Research> UpdateResearchAsync(Research research);
        Task DeleteResearchAsync(int id);

    }
}
