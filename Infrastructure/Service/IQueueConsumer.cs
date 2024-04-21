namespace Infrastructure.Service;

internal interface IQueueConsumer
{
    void SetQueue(string queueName);
    void SetupConsumer();
}