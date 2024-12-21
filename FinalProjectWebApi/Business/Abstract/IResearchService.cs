using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IResearchService
    {

        Task<List<Research>> GetResearchesAsync();
        Task<List<Research>> GetResearchesByUserIdAsync(int userId);
        Task<PagingResult<Research>> GetPagedResearchesAsync(int pageNumber, int pageSize);
        Task<PagingResult<Research>> GetPagedResearhesByUserIdAsync(
            int userId,
            int pageNumber,
            int pageSize,
            string? keyword,
            int? categoryId,
            DateTime? minDate,
            DateTime? maxDate

            );
        Task<PagingResult<Research>> GetCompletedResearchesAsync(
              int pageNumber,
              int pageSize,
              int? categoryId,
              string? keyword,
              DateTime? minDate,
              DateTime? maxDate
            );
        Task<Research> GetResearchByIdAsync(int id);
        Task<Research> AddResearchAsync(Research research);
        Task<Research> UpdateResearchAsync(int id, Research research);

        Task DeleteResearchAsync(int id);

    }
}
