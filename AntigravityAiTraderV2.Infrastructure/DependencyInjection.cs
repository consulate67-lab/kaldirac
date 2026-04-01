using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Interfaces;
using AntigravityAiTraderV2.Infrastructure.Data;
using AntigravityAiTraderV2.Infrastructure.Repositories;
using AntigravityAiTraderV2.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AntigravityAiTraderV2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddSingleton<IConnectionMultiplexer>(sp => 
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection") ?? "localhost:6379"));

        services.AddScoped<IMarketDataRepository, MarketDataRepository>();
        services.AddScoped<ISignalRepository, SignalRepository>();

        services.AddScoped<IMarketDataCollector, MockMarketDataService>();

        services.AddHttpClient<INotificationService, TelegramNotificationService>();

        return services;
    }
}
