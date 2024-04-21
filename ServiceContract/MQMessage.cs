namespace ServiceContract;

public class RabbitMQMessage
{
    public string Message { get; set; }
    public ulong DeliveryTag { get; set; }
}