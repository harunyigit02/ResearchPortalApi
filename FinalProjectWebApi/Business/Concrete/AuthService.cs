using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalProjectWebApi.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository userRepository, IConfiguration configuration)
        {
            _authRepository = userRepository;
            _configuration = configuration;
        }

        public async Task Register(string email, string password)
        {
            // Kullanıcı zaten var mı kontrol et
            if (await _authRepository.GetUserByUserName(email) != null)
            {
                throw new Exception("Kullanıcı zaten kayıtlı.");
            }

            // Şifre Hash ve Salt oluştur
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Role = "User"
            };

            // Kullanıcıyı kaydet
            await _authRepository.AddUser(user);
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _authRepository.GetUserByUserName(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // Şifre doğrula
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new Exception("Şifre yanlış.");

            // Token döndür
            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], // appsettings ile aynı
            audience: _configuration["Jwt:Audience"], // appsettings ile aynı
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<User>> GetUsersAsync()
        {
             return await _authRepository.GetAllAsync();
        }

        public async Task UpdateUserRoleAsync(int userId, string newRole)
        {
            var user = await _authRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            user.Role = newRole; // Kullanıcı rolünü güncelle
            await _authRepository.UpdateAsync(user); // Veritabanında güncelleme
        }

    }

}
