using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class AnswerRepository : IAnswerRepository
    {

        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context) 
        {
            _context = context;

        }
        public async Task<Answer> AddAsync(Answer answer)
        {
             await _context.Answers.AddAsync(answer);
             await _context.SaveChangesAsync();
             return answer;
            
            
        }
        public async Task<List<Answer>> AddAnswersAsync(List<Answer> answers)
        {


            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Set<Answer>().AddRangeAsync(answers);
                    await _context.SaveChangesAsync();

                    // Eğer her şey başarılıysa transaction'ı commit et
                    await transaction.CommitAsync();

                    return answers;
                }
                catch (Exception)
                {
                    // Bir hata olursa tüm işlemi geri al (rollback)
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer != null) 
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Answer>> GetAllAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer> GetByIdAsync(int id)
        {
            return await _context.Answers.FindAsync(id);
            
        }

        public async Task<Answer> UpdateAsync(int id,Answer answer)
        {
            answer = await _context.Answers.FindAsync(id);
            if (answer != null) 
            {
                _context.Answers.Update(answer);
                await _context.SaveChangesAsync();
                
            }
            return answer;
            
            
        }

        public async Task<List<UserAnswerDto>> GetAnswersGroupByUsersAsync(int researchId)
        {
            var result = _context.Answers
                .Include(a => a.Question)
                .Include(a => a.Option)
                .Include(a => a.User)
                .Where(a => a.Question.ResearchId == researchId)
                .GroupBy(a => a.ParticipantId)
                .Select(a => new UserAnswerDto
                {
                    UserId = a.Key,
                    Answers = a.OrderBy(a => a.ParticipatedAt).ToList()
                }
                )
                .ToListAsync();
            return await result;
        }
    }
}
