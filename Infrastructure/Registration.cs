using Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using ServiceContract;

namespace Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this ServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddRabbitMq(configuration);
    }
    
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitmqSettings = new RabbitMQSettings();
        configuration.Bind(RabbitMQSettings.Section, rabbitmqSettings); 
        
        services.AddSingleton(sp => new ConnectionFactory(){
            Uri=rabbitmqSettings.RabbitMqConnectionString,
               RequestedHeartbeat = TimeSpan.FromSeconds(15),         
               AutomaticRecoveryEnabled=true
        });
        services.AddTransient<IRabbitMqConnection, RabbitMqConnection>();
        services.AddTransient<IRabbitMqConsumer, RabbitMqConsumer>();
        services.AddTransient<ISenderFactory, SenderFactory >();
        services.AddTransient<IConsumerFactory, ConsumerFactory>();
        services.AddTransient<IRabbitMqSender, RabbitMqSender>();
        services.AddTransient<IRabbitMqConsumer, RabbitMqConsumer>();
        return services;
    }
}