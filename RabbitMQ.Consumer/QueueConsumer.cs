using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        public static void Consumer(IModel chanel)
        {
            chanel.QueueDeclare("demo-queue",
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null);

            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            chanel.BasicConsume("demo-queue", true, consumer);
        }
    }
}
