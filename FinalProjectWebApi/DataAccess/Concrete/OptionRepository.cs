using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public OptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Option> AddAsync(Option option)
        {
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task DeleteAsync(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option != null)
            {
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return await _context.Options.Include(o=>o.Answers).ToListAsync();
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await _context.Options.Include(o => o.Answers).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Option option)
        {

        }
    }
}
