using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.DataAccess.Abstract;
using FinalProjectWebApi.Entities.Abstract;
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
        private readonly ITemporaryUserRepository _temporaryUserRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(IAuthRepository userRepository, IConfiguration configuration, ITemporaryUserRepository temporaryUserRepository, IEmailService emailService)
        {
            _authRepository = userRepository;
            _configuration = configuration;
            _temporaryUserRepository = temporaryUserRepository;
            _emailService = emailService;
        }

        public async Task Register(string email, string password)
        {
            // Kullanıcı zaten var mı kontrol et
            if (await _temporaryUserRepository.GetUserByUserName(email) != null && await _authRepository.GetUserByUserName(email) != null)
            {
                throw new Exception("Kullanıcı zaten kayıtlı.");
            }

            // Şifre Hash ve Salt oluştur
            using var hmac = new HMACSHA512();
            var verificationCode = GenerateVerificationCode();
            var tempUser = new TemporaryUser
            {
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Role = "User",
                VerificationCode = verificationCode,
                CreatedAt = DateTime.UtcNow
            };

            // Kullanıcıyı geçici kaydet.
            await _temporaryUserRepository.AddAsync(tempUser);

            string emailBody = $@"
            Merhaba,<br/><br/>
            Hesabınızı doğrulamak için aşağıdaki doğrulama kodunu kullanabilirsiniz:<br/><br/>
            <b>{verificationCode}</b><br/><br/>
            Bu kod 5 dakika boyunca geçerlidir.
           ";

            await _emailService.SendEmailAsync(email, "Hesap Doğrulama Kodu", emailBody);


        }
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // 6 haneli rastgele sayı
        }

        public async Task VerifyEmail(string email, string verificationCode)
        {
            // TemporaryUser tablosunda email'e göre kullanıcıyı bul
            var tempUser = await _temporaryUserRepository.GetUserByUserName(email);
            if (tempUser == null)
            {
                throw new Exception("Geçici kullanıcı bulunamadı.");
            }
            if ((DateTime.UtcNow - tempUser.CreatedAt).TotalMinutes > 5)
            {
                // 5 dakika geçmişse, kaydı sil ve hata döndür
                await _temporaryUserRepository.DeleteAsync(tempUser.Id);
                throw new Exception("Doğrulama süresi dolmuş. Kayıt silindi.");
            }

            // VerificationCode kontrolü
            if (tempUser.VerificationCode != verificationCode)
            {
                throw new Exception("Doğrulama kodu hatalı.");
            }

            // Kullanıcıyı User tablosuna taşı
            var user = new User
            {
                Email = tempUser.Email,
                PasswordHash = tempUser.PasswordHash,
                PasswordSalt = tempUser.PasswordSalt,
                Role = tempUser.Role
            };

            await _authRepository.AddUser(user);

            // TemporaryUser kaydını sil
            await _temporaryUserRepository.DeleteAsync(tempUser.Id);
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

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<PagingResult<UserManageDto>> GetUsersAsync(int pageNumber, int pageSize, string? roleFilter, string? keyword, DateTime? minDate, DateTime? maxDate)
        {
            // Sayfalama, arama ve filtreleme işlemlerini gerçekleştiren metoda çağrı yapıyoruz.
            var result = await _authRepository.GetUsersPagedAsync(pageNumber, pageSize, roleFilter, keyword,minDate,maxDate);

            // DTO dönüşümü
            var userDtos = result.Items.Select(x => new UserManageDto
            {
                Id = x.Id,
                Email = x.Email,
                Role = x.Role,
            }).ToList();

            // PagingResult dönüyoruz
            return new PagingResult<UserManageDto>
            {
                Items = userDtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
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

        public async Task<UserManageDto> GetUserByUserIdAsync(int userId)
        {
            var user = await _authRepository.GetByIdAsync(userId); // await ekledik

            // Eğer kullanıcı bulunamazsa, uygun bir değer döndürmek için null kontrolü ekliyoruz
            if (user == null)
            {
                // Burada null döndürebilir veya bir hata fırlatabilirsiniz.
                // Örneğin:
                throw new Exception("User not found");
            }

            return new UserManageDto
            {
                Id = user.Id,         // user nesnesinden Id alıyoruz
                Email = user.Email,   // user nesnesinden Email alıyoruz
                Role = user.Role,     // user nesnesinden Role alıyoruz
            };




        }
    }
}

