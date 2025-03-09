using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.DataAccess.Concrete;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Concrete
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        public AnswerService(IAnswerRepository answerRepository) 
        {
            _answerRepository = answerRepository;
        }
        public async Task<Answer> AddAnswerAsync(Answer answer)
        {
              return await _answerRepository.AddAsync(answer);
             
        }
        public async Task<List<Answer>> AddAnswersAsync(List<Answer> answers, int userId)
        {

            foreach (var answer in answers) 
            {
                if (userId == null)
                {
                    throw new ArgumentNullException(nameof(userId));
                }
                answer.ParticipantId = userId;

            }
            if (answers == null || answers.Count == 0)
            {
                throw new ArgumentException("Answer list cannot be null or empty.");
            }

             return await _answerRepository.AddAnswersAsync(answers);
        }

        public async Task<Answer> DeleteAnswerAsync(int id)
        {
            var answer= await _answerRepository.GetByIdAsync(id);
            if (answer != null) 
            {
                return null;
            }
            await _answerRepository.DeleteAsync(id);
            return answer;

        }

        public Task<Answer> GetAnswerByIdAsync(int id)
        {
            return _answerRepository.GetByIdAsync(id);
        }

        public Task<List<Answer>> GetAnswersAsync()
        {
            return _answerRepository.GetAllAsync();
        }

        public async Task<List<ResearchAnswerDto>> GetAnswersGroupByUsersAsync(int researchId)
        {
            return await _answerRepository.GetAnswersGroupByUsersAsync(researchId);
        }

        public async Task<Answer> UpdateAnswerAsync(int id, Answer answer)
        {
            answer = await _answerRepository.GetByIdAsync(id);
            if (answer != null)
            {
                return null;
            }
            await _answerRepository.UpdateAsync(id,answer);
            return answer;
        }

        public async Task<List<OptionFilterDto>> GetTargetQuestionResultsAsync(int optionId, int questionId)
        {
            return await _answerRepository.GetQuestionParticipantPercentage(optionId, questionId);
        }
    }
}
