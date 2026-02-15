using Bot1.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ExpirationWorker : BackgroundService
{
    private readonly IServiceProvider _services;

    public ExpirationWorker(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[Timer] Background Worker Started...");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IServerService>();
                    await service.CleanupExpiredEntriesAsync();
                    Console.WriteLine("[Timer] Cleanup method executed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Timer Error] {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
