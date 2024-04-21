namespace Infrastructure.Service;

internal interface IQueueSetter
{
    void SetQueue(string queueName);
}