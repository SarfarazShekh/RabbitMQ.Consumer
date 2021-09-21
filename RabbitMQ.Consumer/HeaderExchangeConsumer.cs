using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consumer(IModel chanel)
        {
            chanel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            chanel.QueueDeclare("demo-header-queue",
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);
            var header = new Dictionary<string, object> { { "account", "new" } };
            chanel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty, header);
            chanel.BasicQos(0, 10, false);
            
            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            chanel.BasicConsume("demo-header-queue", true, consumer);
        }
    }
}
