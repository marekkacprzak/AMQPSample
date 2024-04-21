using System.ComponentModel.Design;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceContract;

namespace Application;

public class Core : IDisposable
{
    private readonly ServiceProvider _serviceProvider = ServiceProviderBuild();

    private static ServiceProvider ServiceProviderBuild()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
           // .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .Build();

        return new ServiceCollection()
            .AddRabbitMq(configuration)
            .BuildServiceProvider();
    }

    public ISenderFactory GetSenderFactory()
    {
        return _serviceProvider.GetRequiredService<ISenderFactory>();
    }

    public IConsumerFactory GetConsumerFactory()
    {
        return _serviceProvider.GetRequiredService<IConsumerFactory>();
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
    }
}