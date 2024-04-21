using RabbitMQ.Client;
using ServiceContract;

namespace Infrastructure.Service;

internal class SenderFactory(IRabbitMqConnection connection, IRabbitMqSender sender) : ISenderFactory
{
    public IRabbitMqSender CreateQueue(string queueName)
    {
        connection.GetChannel().QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var queueSetter = sender as IQueueSetter;
        queueSetter?.SetQueue(queueName);
        return sender;
    }
}