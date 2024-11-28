using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class TemporaryUserRepository:ITemporaryUserRepository
    {
        private readonly ApplicationDbContext _context;

        public TemporaryUserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<TemporaryUser> GetByIdAsync(int id)
        {
            return await _context.TemporaryUsers.FindAsync(id);  
        }
        public async Task<TemporaryUser> GetUserByUserName(string email)
        {
            return await _context.TemporaryUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<TemporaryUser>> GetExpiredUsersAsync(DateTime expirationThreshold)
        {
            return await _context.TemporaryUsers
                .Where(user => user.CreatedAt < expirationThreshold)
                .ToListAsync();
        }

        public async Task AddAsync(TemporaryUser temporaryUser)
        {
            await _context.TemporaryUsers.AddAsync(temporaryUser);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var temporaryUser = await _context.TemporaryUsers.FindAsync(id);
            if (temporaryUser != null)
            {
                _context.TemporaryUsers.Remove(temporaryUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
