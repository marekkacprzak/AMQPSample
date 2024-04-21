using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceContract;

namespace Infrastructure.Service;

internal class RabbitMqConsumer(IRabbitMqConnection connection) : IRabbitMqConsumer, IQueueConsumer
{ 
    private string? _queueName;
    public event EventHandler<string?>? Received;

    void IQueueConsumer.SetupConsumer()
    {
        var consumer = new EventingBasicConsumer(connection.GetChannel());
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Received?.Invoke(this, message);
        };
        connection.GetChannel().BasicConsume(queue: _queueName,
            autoAck: true,
            consumer: consumer);
    }

    void IQueueConsumer.SetQueue(string queueName)
    {
        if (!string.IsNullOrWhiteSpace(_queueName))
        {
            throw new ArgumentException("Queue name arleady set.");
        }
        _queueName = queueName;
    }
}