using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<List<UserAnswerDto>> GetAnswersGroupByUsersAsync(int researchId);
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<List<Answer>> AddAnswersAsync(List<Answer> answers,int userId);
        Task<Answer> UpdateAnswerAsync(int id,Answer answer);
        Task<Answer> DeleteAnswerAsync(int id);

    }
}
