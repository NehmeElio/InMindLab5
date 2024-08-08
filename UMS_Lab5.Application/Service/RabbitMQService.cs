using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using UMS_Lab5.Application.Service;

namespace EnrollmentService.Application.Service;

public class RabbitMQService:IRabbitMQService
{
    private readonly string _hostname;
    private readonly string _queueName;
    private readonly IConnection _connection;

    public RabbitMQService(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:HostName"];
        _queueName = configuration["RabbitMQ:QueueName"];
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName="guest",
            Password = "guest",
            VirtualHost = "/",
        };
        _connection = factory.CreateConnection();
    }

    public void PublishMessage(string message)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        //var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
    }
}