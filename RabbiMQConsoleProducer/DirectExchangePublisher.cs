using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbiMQConsoleProducer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);
               
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Counr: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-direct-exchange","account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
