using FinalProjectWebApi.DataAccess.Abstract;
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
    }
}
