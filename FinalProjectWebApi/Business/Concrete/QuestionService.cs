using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            

            return await _questionRepository.AddAsync(question);

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

        public Task<Question> UpdateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
