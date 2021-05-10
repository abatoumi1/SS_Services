using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbiMQConsoleConsumer
{
   static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //  QueueConsumer.Consumer(channel);
            // DirectExchangeConsumer.Consumer(channel);
            //PreFeshCountConsumer.Consumer(channel);
            //TopicExchangeConsumer.Consumer(channel);
            //HeaderExchangeConsumer.Consumer(channel);
            FanOutExchangeConsumer.Consumer(channel);
            //Console.ReadKey();
        }
    }
}
