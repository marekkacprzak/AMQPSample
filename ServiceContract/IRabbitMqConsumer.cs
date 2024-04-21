namespace ServiceContract;

public interface IRabbitMqConsumer
{
    event EventHandler<string?> Received;
}
