using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<List<ResearchAnswerDto>> GetAnswersGroupByUsersAsync(int researchId);
        Task<List<OptionFilterDto>> GetTargetQuestionResultsAsync(List<int> optionIds, int questionId);
        Task<List<OptionFilterDto>> GetAllQuestionResultsAsync(List<int> optionIds, int researchId);
        Task<Answer> AddAnswerAsync(Answer answer);
        Task<List<Answer>> AddAnswersAsync(List<Answer> answers,int userId);
        Task<Answer> UpdateAnswerAsync(int id,Answer answer);
        Task<Answer> DeleteAnswerAsync(int id);

    }
}
