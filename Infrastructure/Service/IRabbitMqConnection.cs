using RabbitMQ.Client;

namespace Infrastructure.Service;

internal interface IRabbitMqConnection: IDisposable
{
    IModel GetChannel();
}