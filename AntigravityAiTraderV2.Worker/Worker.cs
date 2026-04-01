using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Antigravity AI Trader V2 Worker started at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var marketDataCollector = scope.ServiceProvider.GetRequiredService<IMarketDataCollector>();
                var marketDataRepository = scope.ServiceProvider.GetRequiredService<IMarketDataRepository>();
                var signalEngineService = scope.ServiceProvider.GetRequiredService<ISignalEngineService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                // 1. Fetch Mock Data
                var newData = await marketDataCollector.FetchMarketDataAsync();

                // 2. Save Data & Analyze
                foreach (var data in newData)
                {
                    // Save
                    await marketDataRepository.AddAsync(data);

                    // Analyze
                    var signal = await signalEngineService.AnalyzeAndGenerateSignalAsync(data.Symbol);

                    if (signal != null)
                    {
                        _logger.LogInformation("Signal Generated for {Symbol}: {SignalType} (Confidence: {Confidence})", 
                            signal.Symbol, signal.SignalType, signal.Confidence);
                            
                        // 3. Notify via Telegram
                        await notificationService.SendSignalNotificationAsync(signal);
                    }
                }

                _logger.LogInformation("Cycle completed at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the worker execution.");
            }

            // Wait 10 seconds before next cycle
            await Task.Delay(10000, stoppingToken);
        }
    }
}
