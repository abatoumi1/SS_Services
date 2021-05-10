using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbiMQConsoleProducer
{
    public static class FanOutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout,arguments: ttl);
               
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Counr: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-fanout-exchange",string.Empty, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
