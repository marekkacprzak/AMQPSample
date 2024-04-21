using var application=new Application.Core();
var consumerFactory = application.GetConsumerFactory();
var consumer1 = consumerFactory.CreateQueue("hello");

var consumerFactory2 = application.GetConsumerFactory();
var consumer2 = consumerFactory2.CreateQueue("hello");

var random=new Random();
consumer1.Received += (sender, rabbitMqMessage) =>
{
    if (int.TryParse(rabbitMqMessage.Message, out var count))
    {
        if (count % 3 != (0 + random.NextInt64(2)))
        {
            Console.WriteLine($"Consumer1 Acknowladge {count}");
            consumer1.Acknowladge(rabbitMqMessage.DeliveryTag);
        }
        else
        {
            Console.WriteLine($"Consumer1 Reject {count}");
            consumer1.Reject(rabbitMqMessage.DeliveryTag);
        }
    }
    else
    {
        Console.WriteLine($"Consumer1 Received {rabbitMqMessage.Message}");
        consumer1.Acknowladge(rabbitMqMessage.DeliveryTag);
    }
};
consumer2.Received += (sender, rabbitMqMessage) =>
{
    if (int.TryParse(rabbitMqMessage.Message, out var count))
    {
        if (count % 3 != (0 + random.NextInt64(2)))
        {
            Console.WriteLine($"Consumer2 Acknowladge {count}");
            consumer2.Acknowladge(rabbitMqMessage.DeliveryTag);
        }
        else
        {
            Console.WriteLine($"Consumer2 Reject {count}");
            consumer2.Reject(rabbitMqMessage.DeliveryTag);
        }
    }
    else
    {
        Console.WriteLine($"Consumer2 Received {rabbitMqMessage.Message}");
        consumer2.Acknowladge(rabbitMqMessage.DeliveryTag);
    }
};
consumer2.StartConsuming();
consumer1.StartConsuming();
await Task.Delay(30000);
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();