namespace ServiceContract;

public interface IRabbitMqConsumer
{
    event EventHandler<RabbitMQMessage?> Received;
    void Acknowladge(ulong deliveryTag);
}
