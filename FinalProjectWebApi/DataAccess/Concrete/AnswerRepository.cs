using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public async Task<Answer> UpdateAsync(int id, Answer answer)
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
            var questions = await _context.Questions
                .Where(q => q.ResearchId == researchId)
                .Include(q => q.Options)
                .ToListAsync();

            var questionIds = questions.Select(q => q.Id).ToList();

            var answers = await _context.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .Include(a => a.Option)
                .ToListAsync(); // Cevapları bellek içine çekiyoruz.

            // Sadece cevap vermiş kullanıcıları alıyoruz
            var participantIds = answers.Select(a => a.ParticipantId).Distinct().ToList();
            var participants = await _context.Users
                .Where(u => participantIds.Contains(u.Id)) // Sadece cevabı olan kullanıcıları getiriyoruz
                .ToListAsync();

            var result = participants.Select(p => new ResearchAnswerDto
            {
                QuestionAnswers = questions
                    .Where(q => answers.Any(a => a.ParticipantId == p.Id && a.QuestionId == q.Id)) // Kullanıcının cevapladığı soruları filtrele
                    .Select(q =>
                    {
                        var answer = answers.FirstOrDefault(a => a.ParticipantId == p.Id && a.QuestionId == q.Id);
                        return new QuestionAnswerDto
                        {
                            QuestionId = q.Id,
                            QuestionText = q.QuestionText,
                            Options = q.Options.Select(o => new OptionDTO
                            {
                                OptionId = o.Id,
                                OptionText = o.OptionText
                            }).ToList(),
                            SelectedOptionId = answer?.OptionId ?? 0, // Seçilen cevap
                            ParticipatedAt = answer?.ParticipatedAt ?? DateTime.MinValue // Cevaplanma zamanı
                        };
                    }).ToList()
            }).ToList();

            return result;
        }

        public async Task<List<OptionFilterDto>> GetQuestionParticipantPercentage(List<int> optionIds, int questionId)
        {
            // Seçeneklere göre kullanıcıları filtrele
            var filterUsers = await _context.Answers
                .Where(a => optionIds.Contains(a.OptionId))
                .Select(a => a.User)
                .Distinct()
                .ToListAsync(); 

            // Şimdi bu kullanıcıların ilgili soruya verdikleri cevapları gruplandır
            var targetQuestionResponses = _context.Answers
                .Where(a => a.QuestionId == questionId && filterUsers.Contains(a.User)) // Bu artık bellekte çalışacak
                .GroupBy(a => a.OptionId);

            var totalResponses = await _context.Answers
                .Where(a => a.QuestionId == questionId && filterUsers.Contains(a.User))
                .CountAsync();

            var result = await targetQuestionResponses
                .Select(g => new OptionFilterDto
                {
                    QuestionId = questionId,
                    OptionId = g.Key,
                    Count = g.Count(),
                    Percentage = totalResponses == 0 ? "0%" : $"{(g.Count() * 100.0 / totalResponses):0.##}%"
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<OptionFilterDto>> GetResearchParticipantPercentage(List<int> optionIds, int researchId)
        {
            // 1. Araştırmaya ait tüm soruları al
            var questions = await _context.Questions
                .Where(q => q.ResearchId == researchId)  // Araştırma id'ye göre filtrele
                .ToListAsync();
            List<OptionFilterDto> allResult = new List<OptionFilterDto>(); 
            

            foreach (var question in questions) 
            {
                var filterUsers = await _context.Answers
               .Where(a => optionIds.Contains(a.OptionId))
               .Select(a => a.User)
               .Distinct()
               .ToListAsync();

                // Şimdi bu kullanıcıların ilgili soruya verdikleri cevapları gruplandır
                var targetQuestionResponses = _context.Answers
                    .Where(a => a.QuestionId == question.Id && filterUsers.Contains(a.User)) // Bu artık bellekte çalışacak
                    .GroupBy(a => a.OptionId);

                var totalResponses = await _context.Answers
                    .Where(a => a.QuestionId == question.Id && filterUsers.Contains(a.User))
                    .CountAsync();

                var result = await targetQuestionResponses
                    .Select(g => new OptionFilterDto
                    {
                        QuestionId = question.Id,
                        OptionId = g.Key,
                        Count = g.Count(),
                        Percentage = totalResponses == 0 ? "0%" : $"{(g.Count() * 100.0 / totalResponses):0.##}%"
                    })
                    .ToListAsync();
                allResult.AddRange(result);
            }
            return allResult;
            

           
        }


        public async Task<List<OptionFilterDto>> GetQuestionParticipantPercentage2(List<int> optionIds, int questionId)
        {
            // Seçeneklere göre kullanıcıları filtrele
            var filterUsers = await _context.Answers
                .Where(a => optionIds.Contains(a.OptionId))
                .Select(a => a.User)
                .Distinct()
                .ToListAsync();

            // Şimdi bu kullanıcıların ilgili soruya verdikleri cevapları gruplandır
            var targetQuestionResponses = _context.Answers
                .Where(a => a.QuestionId == questionId && filterUsers.Contains(a.User)) // Bu artık bellekte çalışacak
                .GroupBy(a => a.OptionId);

            var totalResponses = await _context.Answers
                .Where(a => a.QuestionId == questionId && filterUsers.Contains(a.User))
                .CountAsync();

            var result = await targetQuestionResponses
                .Select(g => new OptionFilterDto
                {
                    QuestionId = questionId,
                    OptionId = g.Key,
                    Count = g.Count(),
                    Percentage = totalResponses == 0 ? "0%" : $"{(g.Count() * 100.0 / totalResponses):0.##}%"
                })
                .ToListAsync();

            return result;
        }








    }
}