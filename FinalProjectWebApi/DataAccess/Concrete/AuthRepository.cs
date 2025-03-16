using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalProjectWebApi.DataAccess.Concrete
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagingResult<UserManageDto>> GetUsersPagedAsync(int pageNumber, int pageSize, string? roleFilter, string? keyword)
        {
            var query = _context.Users.AsQueryable();

            // Rol filtresi
            if (!string.IsNullOrEmpty(roleFilter))
            {
                query = query.Where(u => u.Role == roleFilter);
            }

            // Arama (Email veya Role içinde arama)
            if (!string.IsNullOrEmpty(keyword))
            {
                var loweredKeyword = keyword.ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(loweredKeyword) || u.Role.ToLower().Contains(loweredKeyword));
            }

            // Toplam öğe sayısı (sayfalama için)
            var totalItems = await query.CountAsync();

            // Sayfalama işlemi
            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // DTO'ya dönüştürme
            var userDtos = users.Select(u => new UserManageDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return new PagingResult<UserManageDto>
            {
                Items = userDtos,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUserName(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
