using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
   public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            //QueueProducer.publish(chanel);
            //DirectExchangeProducer.Publish(chanel);

            //
            //TopicExchangeProducer.Publish(chanel);
            //
            //HeaderExchangeProducer.Publish(chanel);

            //

            FanoutExchangeProducer.Publish(chanel);
        }
    }
}
