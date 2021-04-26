using MyBackendService.Models;
using RabbitMQ.Client;
using System.Text;

namespace MyBackendService.Services
{
    public class MessageQueueService
    {
        public static void Send(TopicKey topicKey, string message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: topicKey.ToString(),
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: topicKey.ToString(),
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch
            {
            }
        }
    }
}