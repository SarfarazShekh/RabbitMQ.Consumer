using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class DirectExchangeProducer
    {
        public static void Publish(IModel chanel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            chanel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct,arguments:ttl);
            int count = 0;
            while (true)
            {
                var message = new { name = "Producer", message = $"Hello! count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                chanel.BasicPublish("demo-direct-exchange", "account.init",null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
