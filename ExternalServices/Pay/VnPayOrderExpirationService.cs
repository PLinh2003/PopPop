using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PopPop.Services.Interfaces;

namespace PopPop.ExternalServices.Pay;

public sealed class VnPayOrderExpirationService : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(1);
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<VnPayOrderExpirationService> _logger;

    public VnPayOrderExpirationService(
        IServiceScopeFactory scopeFactory,
        ILogger<VnPayOrderExpirationService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Interval);
        do
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                await orderService.ExpirePendingVnPayOrdersAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to expire pending VNPay orders.");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
