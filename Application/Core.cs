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
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
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