using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consumer(IModel chanel)
        {
            chanel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);
            chanel.QueueDeclare("demo-direct-queue",
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);
            chanel.QueueBind("demo-direct-queue", "demo-direct-exchange", "account.init");
            chanel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            chanel.BasicConsume("demo-direct-queue", true, consumer);
        }
    }
}
