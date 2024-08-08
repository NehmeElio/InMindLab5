namespace UMS_Lab5.Application.Service;

public interface IRabbitMQService
{
    public void PublishMessage(string message);
}