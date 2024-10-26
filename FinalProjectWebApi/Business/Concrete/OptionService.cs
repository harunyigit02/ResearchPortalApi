using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Concrete
{
    public class OptionService:IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Option> AddOptionAsync(Option option)
        {


            return await _optionRepository.AddAsync(option);

        }

        public async Task<Option> DeleteOptionAsync(int id)
        {
            // 1. Öncelikle silinecek kaydın var olup olmadığını kontrol edelim
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null)
            {
                // Kayıt bulunamadıysa false döndürüyoruz (ya da özel bir hata fırlatabiliriz)
                return null;
            }

            // 2. Kayıt varsa silme işlemini gerçekleştiriyoruz
            await _optionRepository.DeleteAsync(id);

            // 3. İşlem başarılı ise true döndürüyoruz
            return option;
        }


        public async Task<Option> GetOptionByIdAsync(int id)
        {
            return await _optionRepository.GetByIdAsync(id);
        }

        public async Task<List<Option>> GetOptionsAsync()
        {
            return await _optionRepository.GetAllAsync();
        }

        public Task<Option> UpdateOptionAsync(Option option)
        {
            throw new NotImplementedException();
        }
    }
}
