using EMA.DB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EMA.Web.Workers
{
    public class InitWorker : IHostedService
    {
        private readonly ILogger<InitWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public InitWorker(ILogger<InitWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // DBのマイグレーションを行う
            using (var scope = _scopeFactory.CreateScope())
            {
                _logger.LogInformation("migrate database start");

                DatabaseFacade database = scope.ServiceProvider.GetRequiredService<EmaDbContextBase>().Database;

                // 未適用のマイグレーションが存在する場合
                var pendingMigrations = await database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    // マイグレーションを実行
                    database.SetCommandTimeout(TimeSpan.FromMinutes(1));
                    await database.MigrateAsync(cancellationToken);
                    _logger.LogInformation("migration is success.");
                }
                // すべてのマイグレーションが適用されている場合
                else
                {
                    _logger.LogInformation("migration is skipped.");
                }

                _logger.LogInformation("migrate database finish");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
