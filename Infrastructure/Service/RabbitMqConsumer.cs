using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceContract;

namespace Infrastructure.Service;


internal class RabbitMqConsumer(IRabbitMqConnection connection) : IRabbitMqConsumer, IQueueConsumer
{ 
    private string? _queueName;
    public event EventHandler<RabbitMQMessage>? Received;

    public void Acknowladge(ulong deliveryTag)
    {
        connection.GetChannel().BasicAck(deliveryTag,false);
    }

    public void Reject(ulong deliveryTag)
    {
        connection.GetChannel().BasicReject(deliveryTag, true);
    }

    public void StartConsuming()
    {
        if (Received==null)
        {
            throw new ArgumentException("Consuming event handler not set.");
        }
        var consumer = new EventingBasicConsumer(connection.GetChannel());
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var rabbitMQMessage = new RabbitMQMessage
            {
                Message = Encoding.UTF8.GetString(body),
                DeliveryTag = ea.DeliveryTag
            };
            Received.Invoke(this, rabbitMQMessage);
        };
        connection.GetChannel().BasicConsume(queue: _queueName,
            autoAck: false,
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