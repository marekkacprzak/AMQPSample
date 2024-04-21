using var application=new Application.Core();
var senderFactory = application.GetSenderFactory();
var sender = senderFactory.CreateQueue("hello");
var quit="";
while (quit?.ToLower()!="q")
{
    sender.Send($"Hello World! Typed: {quit}");
    Console.WriteLine(" [x] Sent 'Hello World! Type q to quit.");
    quit = Console.ReadLine();
}