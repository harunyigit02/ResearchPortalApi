using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IQuestionRepository
    {
        Task<Question> GetByIdAsync(int id);
        Task<List<Question>> GetAllAsync();
        Task<Question> AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}
