using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ParticipantInfoRepository:IParticipantInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public ParticipantInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ParticipantInfo> AddAsync(ParticipantInfo participantInfo)
        {
            await _context.ParticipantInfos.AddAsync(participantInfo);
            await _context.SaveChangesAsync();
            return participantInfo;
        }

        public async Task DeleteAsync(int id)
        {
            var participantInfo = await _context.ParticipantInfos.FindAsync(id);
            if (participantInfo != null)
            {
                _context.ParticipantInfos.Remove(participantInfo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ParticipantInfo>> GetAllAsync()
        {
            return await _context.ParticipantInfos.ToListAsync();
        }

        public async Task<ParticipantInfo> GetByIdAsync(int id)
        {
            return await _context.ParticipantInfos.FindAsync(id);
        }

        public async Task<ParticipantInfo> GetByUserIdAsync (int userId)
        {
            return await _context.ParticipantInfos
               .FindAsync(userId);
        }

        public Task UpdateAsync(ParticipantInfo participantInfo)
        {
            throw new NotImplementedException();
        }
    }
}
