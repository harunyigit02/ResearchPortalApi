using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<Answer> UpdateAnswerAsync(int id,Answer answer);
        Task<Answer> DeleteAnswerAsync(int id);

    }
}
