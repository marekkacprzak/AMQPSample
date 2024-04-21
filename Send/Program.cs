using var application=new Application.Core();
var senderFactory = application.GetSenderFactory();
var sender = senderFactory.CreateQueue("hello");
var quit="";
var counter=0;
while (quit?.ToLower()!="q")
{
    sender.Send($"Hello World! Typed: {quit}");
    await Task.Delay(1000);
    Console.WriteLine(" [x] Sent 'Hello World! Type q to quit.");
    quit = Console.ReadLine();
    if (counter>1000)
    {
        break;
    }
}