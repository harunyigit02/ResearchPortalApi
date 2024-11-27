using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.DataAccess.Abstract
{
    public interface IParticipantInfoRepository
    {
        Task<ParticipantInfo> GetByIdAsync(int id);
        Task<List<ParticipantInfo>> GetAllAsync();
        Task<ParticipantInfo> GetByUserIdAsync(int userId);
        Task<ParticipantInfo> AddAsync(ParticipantInfo participantInfo);
        Task UpdateAsync(ParticipantInfo participantInfo);
        Task DeleteAsync(int id);
    }
}
