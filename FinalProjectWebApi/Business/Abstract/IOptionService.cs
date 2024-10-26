using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Business.Abstract
{
    public interface IOptionService
    {
        Task<List<Option>> GetOptionsAsync();
        Task<Option> GetOptionByIdAsync(int id);
        Task<Option> AddOptionAsync(Option option);
        Task<Option> UpdateOptionAsync(Option option);
        Task<Option> DeleteOptionAsync(int id);
    }
}
