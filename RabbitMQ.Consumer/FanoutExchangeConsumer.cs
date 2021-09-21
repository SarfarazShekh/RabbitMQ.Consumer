using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consumer(IModel chanel)
        {
            chanel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            chanel.QueueDeclare("demo-fanout-queue",
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);
            chanel.QueueBind("demo-fanout-queue", "demo-tofanoutpic-exchange", string.Empty);
            chanel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            chanel.BasicConsume("demo-fanout-queue", true, consumer);
        }
    }
}
