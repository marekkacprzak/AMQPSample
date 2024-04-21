namespace ServiceContract;

public interface ISenderFactory
{
    IRabbitMqSender CreateQueue(string queueName);
}