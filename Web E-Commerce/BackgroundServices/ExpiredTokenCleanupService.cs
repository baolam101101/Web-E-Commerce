using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;

namespace Web_E_Commerce.BackgroundServices
{
    public class ExpiredTokenCleanupService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredTokenCleanupService> logger) : BackgroundService
    {
        // Chạy mỗi 24 giờ
        private readonly TimeSpan _interval = TimeSpan.FromHours(24);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ExpiredTokenCleanupService started.");

            // Chờ 1 phút sau khi app khởi động rồi mới chạy lần đầu
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanupAsync(stoppingToken);
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task CleanupAsync(CancellationToken stoppingToken)
        {
            try
            {
                // BackgroundService không dùng Scoped service trực tiếp
                // phải tạo scope mới mỗi lần chạy
                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var cutoff = DateTime.UtcNow;

                var deleted = await context.RefreshTokens
                    .Where(rt => rt.IsRevoked || rt.ExpiryDate <= cutoff)
                    .ExecuteDeleteAsync(stoppingToken);

                if (deleted > 0)
                    logger.LogInformation(
                        "Token cleanup: removed {Count} expired/revoked tokens at {Time}.",
                        deleted, DateTime.UtcNow);
            }
            catch (OperationCanceledException)
            {
                // App đang shutdown — bình thường, không cần log
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during token cleanup.");
            }
        }
    }
}