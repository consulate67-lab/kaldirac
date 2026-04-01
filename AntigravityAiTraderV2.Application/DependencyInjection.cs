using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AntigravityAiTraderV2.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIndicatorService, IndicatorService>();
        services.AddScoped<IAiEngineService, AiEngineService>();
        services.AddScoped<ISignalEngineService, SignalEngineService>();

        return services;
    }
}
