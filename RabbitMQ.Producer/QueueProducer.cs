using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void publish(IModel chanel)
        {
            chanel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            int count = 0;
            while (true)
            {
                var message = new { name = "Producer", message = $"Hello! count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                chanel.BasicPublish("", "demo-queue", null, body);
                count++;
                Thread.Sleep(1000);
            }
            

        }
    }
}
