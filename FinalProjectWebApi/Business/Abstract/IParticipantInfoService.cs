using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IParticipantInfoService
    {
        Task<List<ParticipantInfo>> GetParticipantInfosAsync();
        Task<ParticipantInfo> GetParticipantInfoByIdAsync(int id);
        Task<ParticipantInfo> GetParticipantInfosByUserIdAsync(int userId);
        Task<ParticipantInfo> AddParticipantInfoAsync(ParticipantInfo participantInfo);
        Task<ParticipantInfo> UpdateParticipantInfoAsync(ParticipantInfo participantInfo);
        Task DeleteParticipantInfoAsync(int id);
    }
}
