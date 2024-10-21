using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Business.Concrete
{
    public class ViewsService : IViewsService
    {
        private readonly ApplicationDbContext _context;

        public ViewsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Views> AddViewsAsync(Views views)
        {
            throw new NotImplementedException();
        }

        public Task DeleteViewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Views>> GetViewsAsync()
        {
            return _context.Views.ToListAsync();
            
        }

        public Task<Views> GetViewsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Views> UpdateViewsAsync(Views views)
        {
            throw new NotImplementedException();
        }
    }
}
