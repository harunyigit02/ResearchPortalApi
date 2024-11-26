using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class ResearchRepository : IResearchRepository
    {

        private readonly ApplicationDbContext _context;

        public ResearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Research> AddAsync(Research research)
        {
            await _context.Researches.AddAsync(research);
            await _context.SaveChangesAsync();
            return research;
        }

        public async Task DeleteAsync(int id)
        {
            var research = await _context.Researches.FindAsync(id);
            if (research != null)
            {
                _context.Researches.Remove(research);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Research>> GetAllAsync()
        {
            return await _context.Researches
                .Include(r=>r.Questions)
                .ThenInclude(q=>q.Options)
                .ToListAsync();
        }
        public async Task<PagingResult<Research>> GetCompletedAsync(
               int pageNumber,
               int pageSize,
               int? categoryId,
               string? keyword)
        {
            var queryable = _context.Researches
                .Include(r => r.Questions)
                .ThenInclude(q => q.Options)
                .Where(r => r.IsCompleted); // Önceden tamamlanmış araştırmaları filtreledik

            // Filtreleme ekliyoruz
            if (!string.IsNullOrEmpty(keyword))
            {
                var lowerKeyword=keyword.ToLower();
                queryable = queryable.Where(r => r.Title.ToLower().Contains(lowerKeyword) || r.Description.ToLower().Contains(lowerKeyword));
            }

            if (categoryId.HasValue)
            {
                queryable = queryable.Where(r => r.CategoryId == categoryId.Value);
            }

            
            

            // Sayfalama işlemi
            var totalItems = await queryable.CountAsync();
            var completedResearches = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Research>
            {
                Items = completedResearches,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
                
            };
        }

        public async Task<Research> GetByIdAsync(int id)
        {
            return await _context.Researches.Include(r=>r.Questions).ThenInclude(q=>q.Options).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<Research>> GetByUserIdAsync(int userId)
        {
            return _context.Researches
                .Where(a => a.PublishedBy == userId).ToList();  // Veritabanında filtreleme
        }

        public async Task UpdateAsync(Research research)
        {
            _context.Researches.Update(research);
            await _context.SaveChangesAsync();
            



        }
        public async Task<PagingResult<Research>> GetResearchesPagedAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Researches.CountAsync();

            var research = await _context.Researches
                .Skip((pageNumber - 1) * pageSize)  // Atlanacak öğe sayısı
                .Take(pageSize)                    // Alınacak öğe sayısı
                .ToListAsync();

            return new PagingResult<Research>
            {
                Items = research,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<PagingResult<Research>> GetPagedResearchesByUserIdAsync(
               int userId,
               int pageNumber,
               int pageSize,
               string? keyword,
               int? categoryId
             )
        {
            var query = _context.Researches
               .Where(a => a.PublishedBy == userId);

            if (!string.IsNullOrEmpty(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                query = query.Where(r => r.Title.ToLower().Contains(lowerKeyword) || r.Description.ToLower().Contains(lowerKeyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(r => r.CategoryId == categoryId.Value);
            }

            

            int totalCount = await query.CountAsync();
            var researches = await query
                .Skip((pageNumber - 1) * pageSize) // Sayfalama için atlama
                .Take(pageSize) // Sayfa boyutunda veri çekme
                .ToListAsync(); // Listeleme
            return new PagingResult<Research>
            {
                Items = researches,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}