using var application=new Application.Core();
var consumerFactory = application.GetConsumerFactory();
var consumer = consumerFactory.CreateQueue("hello");

consumer.Received += async (sender, rabbitMqMessage) =>
{
    var count=int.Parse(rabbitMqMessage.Message);
    Console.WriteLine($"Received {rabbitMqMessage.Message}");
    consumer.Acknowladge(rabbitMqMessage.DeliveryTag);
};
Console.WriteLine("Press [enter] to exit.");
await Task.Delay(30000);
//Console.ReadLine();