namespace ServiceContract;

public interface IRabbitMqSender
{
    void Send(string message);
}