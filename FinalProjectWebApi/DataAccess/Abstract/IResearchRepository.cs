using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IResearchRepository
    {

        Task<Research> GetByIdAsync(int id);
        Task<List<Research>> GetByUserIdAsync(int userId);
        Task<List<Research>> GetAllAsync();
        Task<PagingResult<Research>> GetResearchesPagedAsync(int pageNumber, int pageSize);
        Task<PagingResult<Research>> GetPagedResearchesByUserIdAsync(
               int userId,
               int pageNumber,
               int pageSize,
               string? title,
               int? categoryId,
               bool? isFaceToFace,
               DateTime? publishedAt,
               int? publishedBy);
        Task<PagingResult<Research>> GetCompletedAsync(int pageNumber,int pageSize,string? title,int? categoryId,bool? isFaceToFace,DateTime? publishedAt,int? publishedBy);

        Task<Research> AddAsync(Research research);
        Task UpdateAsync( Research research);
        Task DeleteAsync(int id);
    }
}

