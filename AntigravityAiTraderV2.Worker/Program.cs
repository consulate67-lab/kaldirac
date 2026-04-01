using AntigravityAiTraderV2.Application;
using AntigravityAiTraderV2.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AntigravityAiTraderV2.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddApplication();
                services.AddInfrastructure(hostContext.Configuration);
                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }
}
