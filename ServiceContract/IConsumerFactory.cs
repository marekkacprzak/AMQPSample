namespace ServiceContract;

public interface IConsumerFactory
{
    IRabbitMqConsumer CreateQueue(string queueName);
}