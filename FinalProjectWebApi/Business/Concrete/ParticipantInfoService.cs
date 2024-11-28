using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ParticipantInfoService : IParticipantInfoService
    {
        private readonly IParticipantInfoRepository _participantInfoRepository;

        public ParticipantInfoService(IParticipantInfoRepository participantInfoRepository) 
        {
            _participantInfoRepository = participantInfoRepository;
        }
        public async Task<ParticipantInfo> AddParticipantInfoAsync(ParticipantInfo participantInfo)
        {
            return await _participantInfoRepository.AddAsync(participantInfo);
        }

        public async Task DeleteParticipantInfoAsync(int id)
        {
            await _participantInfoRepository.DeleteAsync(id);
        }

        public async Task<ParticipantInfo> GetParticipantInfoByIdAsync(int id)
        {
            return await _participantInfoRepository.GetByIdAsync(id);
        }

        public async Task<List<ParticipantInfo>> GetParticipantInfosAsync()
        {
            return await _participantInfoRepository.GetAllAsync();
        }

        public async Task<ParticipantInfo> GetParticipantInfosByUserIdAsync(int userId)
        {
            return await _participantInfoRepository.GetByUserIdAsync(userId);
        }



        public async Task<ParticipantInfo> UpdateParticipantInfoAsync(ParticipantInfo participantInfo)
        {
            throw new NotImplementedException();
        }

       
    }
}
