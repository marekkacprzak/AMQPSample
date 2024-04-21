using RabbitMQ.Client;

namespace Infrastructure.Service;

internal class RabbitMqConnection:IRabbitMqConnection
{ 
    private readonly IModel _channel;

    public IModel GetChannel()
    {
        return _channel;
    }
    public RabbitMqConnection(ConnectionFactory factory)
    {
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public void Dispose()
    {
        _channel.Close();
        _channel.Dispose();
    }
}