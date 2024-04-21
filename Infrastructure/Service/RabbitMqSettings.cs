internal class RabbitMQSettings
{
    public const string Section = "RabbitMQ";
    public  string HostName { get; set; } = null!;
    public  string Port { get; set; } = null!;
    public  string UserName { get; set; } = null!;
    public  string Password { get; set; } = null!;
    public Uri RabbitMqConnectionString => new($"amqp://{UserName}:{Password}@{HostName}:{Port}");
}