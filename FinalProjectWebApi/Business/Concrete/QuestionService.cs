using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.DataAccess.Concrete;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IResearchRepository _researchRepository;


        public QuestionService(IQuestionRepository questionRepository, IResearchRepository researchRepository)
        {
            _questionRepository = questionRepository;
            _researchRepository = researchRepository;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            

            return await _questionRepository.AddAsync(question);

        }
        public async Task<bool> IsUserAuthorizedForResearchAsync(int userId,Question question)
        {
            
            var research = await _researchRepository.GetByIdAsync(question.ResearchId);

            return research != null && research.PublishedBy == userId;
        }

        public async Task<Question> DeleteQuestionAsync(int id)
        {
            // 1. Öncelikle silinecek kaydın var olup olmadığını kontrol edelim
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                // Kayıt bulunamadıysa false döndürüyoruz (ya da özel bir hata fırlatabiliriz)
                return null;
            }

            // 2. Kayıt varsa silme işlemini gerçekleştiriyoruz
            await _questionRepository.DeleteAsync(id);

            // 3. İşlem başarılı ise true döndürüyoruz
            return question;
        }


        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _questionRepository.GetByIdAsync(id);
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _questionRepository.GetAllAsync();
        }
        public async Task<List<Question>> GetQuestionsByResearchIdAsync(int researchId)
        {
            return await _questionRepository.GetByResearchIdAsync(researchId);
        }


        public async Task<Question> UpdateQuestionAsync(int id,Question question)
        {
            var existingquestion = await _questionRepository.GetByIdAsync(id);
            if (existingquestion == null)
            {
                throw new ArgumentException("Question not found");
            }

            existingquestion.QuestionText = question.QuestionText;
            existingquestion.Options = question.Options;
            
            await _questionRepository.UpdateAsync(existingquestion);
            return existingquestion;
        }

        public async Task DeleteAsync(int id)
        {
            await _questionRepository.DeleteAsync(id);
        }
    }
}
