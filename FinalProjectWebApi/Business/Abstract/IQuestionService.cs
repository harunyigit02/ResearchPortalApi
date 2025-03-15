using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task<List<Question>> GetQuestionsByResearchIdAsync(int researchId);
        Task<Question> AddQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(int id,Question question);
        Task<Question> DeleteQuestionAsync(int id);
        Task<bool> IsUserAuthorizedForResearchAsync(int userId, Question question);
    }
}
