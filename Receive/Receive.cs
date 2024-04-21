using var application=new Application.Core();
var consumerFactory = application.GetConsumerFactory();
var consumer = consumerFactory.CreateQueue("hello");
var counter=0;
consumer.Received += (sender, message) =>
{
    Console.WriteLine($"[x] Received {counter++} {message}");
};
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();