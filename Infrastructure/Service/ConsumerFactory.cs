using RabbitMQ.Client;
using ServiceContract;

namespace Infrastructure.Service;

internal class ConsumerFactory(IRabbitMqConnection connection, IRabbitMqConsumer consumer) : IConsumerFactory
{
    public IRabbitMqConsumer CreateQueue(string queueName)
    {
        connection.GetChannel()
            .QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        var queueReciver = consumer as IQueueConsumer;
        queueReciver?.SetQueue(queueName);
        return consumer;
    }
}