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

        public async Task<List<ResearchAnswerDto>> GetAnswersGroupByUsersAsync(int researchId)
        {
            var questions = _context.Questions
       .Where(q => q.ResearchId == researchId)
       .Include(q => q.Options)
       .ToList();

            var answers = _context.Answers
                .Where(a => questions.Select(q => q.Id).Contains(a.QuestionId))
                .Include(a => a.Option)
                .ToList();

            var participants = _context.Users.ToList(); // Katılımcıların listesi

            var result = participants.Select(p => new ResearchAnswerDto
            {
                
                QuestionAnswers = questions.Select(q => new QuestionAnswerDto
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                    Options = q.Options.Select(o => new OptionDTO
                    {
                        QuestionId = o.Id,
                        OptionText = o.OptionText
                    }).ToList(),
                    SelectedOptionId = answers.FirstOrDefault(a => a.ParticipantId == p.Id && a.QuestionId == q.Id)?.OptionId ?? 0, // Seçilen seçenek
                    ParticipatedAt = answers.FirstOrDefault(a => a.ParticipantId == p.Id && a.QuestionId == q.Id)?.ParticipatedAt ?? DateTime.MinValue // Cevaplanma Zamanı
                }).ToList()
            }).ToList();
            return result;
        }
    }
}
