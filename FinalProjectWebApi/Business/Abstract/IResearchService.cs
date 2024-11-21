using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchService
    {

        Task<List<Research>> GetResearchesAsync();
        Task<List<Research>> GetResearchesByUserIdAsync(int userId);
        Task<List<Research>> GetCompletedResearchesAsync();
        Task<Research> GetResearchByIdAsync(int id);
        Task<Research> AddResearchAsync(Research research);
        Task<Research> UpdateResearchAsync(int id, Research research);

        Task DeleteResearchAsync(int id);

    }
}
