using FinalProjectWebApi.DataAccess.Abstract;

namespace FinalProjectWebApi.Business.Concrete
{
    public class TemporaryUserCleanupService:BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TemporaryUserCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanupExpiredTemporaryUsers();

                // Görevi belirli bir süre beklet (örneğin her 5 dakikada bir çalıştır)
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task CleanupExpiredTemporaryUsers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // TemporaryUserRepository'yi scope'dan al
                var temporaryUserRepository = scope.ServiceProvider.GetRequiredService<ITemporaryUserRepository>();

                // Şu andan itibaren 5 dakikayı geçmiş kullanıcıları sil
                var expirationThreshold = DateTime.UtcNow.AddMinutes(-5);
                var expiredUsers = await temporaryUserRepository.GetExpiredUsersAsync(expirationThreshold);

                foreach (var user in expiredUsers)
                {
                    await temporaryUserRepository.DeleteAsync(user.Id);
                }
            }
        }
    }
}
