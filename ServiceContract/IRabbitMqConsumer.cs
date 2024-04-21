namespace ServiceContract;

public interface IRabbitMqConsumer
{
    event EventHandler<RabbitMQMessage> Received;
    void StartConsuming();
    void Acknowladge(ulong deliveryTag);
    void Reject(ulong deliveryTag);
}
