using System.Text;
using RabbitMQ.Client;
using ServiceContract;

namespace Infrastructure.Service;

internal class RabbitMqSender(IRabbitMqConnection connection):IRabbitMqSender,IQueueSetter
{
    private string? _queueName;
    public void Send(string message)
    { 
        var body = Encoding.UTF8.GetBytes(message);
        var property = connection.GetChannel().CreateBasicProperties();
        property.Persistent = true;
        connection.GetChannel().BasicPublish(exchange: string.Empty,
            routingKey: _queueName,
            basicProperties: property,
            body: body);
    }

    void IQueueSetter.SetQueue(string queueName)
    {
        if (!string.IsNullOrWhiteSpace(_queueName))
        {
            throw new ArgumentException("Queue name arleady set.");
        }
        _queueName = queueName;
    }
}