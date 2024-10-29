using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ViewsService : IViewsService
    {
        private readonly IViewsRepository _viewsRepository;

        public ViewsService(IViewsRepository viewsRepository)
        {
            _viewsRepository = viewsRepository;
        }

        public async Task<Views> AddViewsAsync(Views views)
        {


            return await _viewsRepository.AddAsync(views);

        }
        public Task DeleteViewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Views>> GetViewsAsync()
        {
            return await _viewsRepository.GetAllAsync();

        }

        public async Task<Views> GetViewsByIdAsync(int id)
        {
            return await _viewsRepository.GetByIdAsync(id);
        }

        public async Task<List<Views>> GetViewsByArticleId(int articleId)
        {
            return await _viewsRepository.GetByArticleIdAsync(articleId);

        }
        public async Task<int> GetViewsCountByArticleIdAsync(int articleId)
        {
            return await _viewsRepository.GetViewsCountByArticleIdAsync(articleId);
        }

        public Task<Views> UpdateViewsAsync(Views views)
        {
            throw new NotImplementedException();
        }
    }
}
