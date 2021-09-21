using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consumer(IModel chanel)
        {
            chanel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            chanel.QueueDeclare("demo-topic-queue",
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);
            chanel.QueueBind("demo-topic-queue", "demo-topic-exchange", "account.*");
            chanel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            chanel.BasicConsume("demo-topic-queue", true, consumer);
        }
    }
}
