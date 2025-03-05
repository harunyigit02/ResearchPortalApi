using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> GetAllAsync();
        Task<Answer> GetByIdAsync(int id);
        Task<List<UserAnswerDto>> GetAnswersGroupByUsersAsync(int researchId);


        Task<Answer> AddAsync(Answer answer);
        Task<List<Answer>> AddAnswersAsync(List<Answer> answers);
        Task<Answer> UpdateAsync(int id,Answer answer);
        Task DeleteAsync(int id);

    }
}
