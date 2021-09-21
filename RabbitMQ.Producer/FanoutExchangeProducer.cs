using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangeProducer
    {
        public static void Publish(IModel chanel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            chanel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Topic, arguments: ttl);
            int count = 0;
            while (true)
            {
                var message = new { name = "Producer", message = $"Hello! count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                var properties = chanel.CreateBasicProperties();
                  properties.Headers  = new Dictionary<string, object> { { "account", "new" } };
                chanel.BasicPublish("demo-fanout-exchange", string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
