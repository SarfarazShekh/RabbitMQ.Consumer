using RabbitMQ.Client;
using System;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            /*This is for create basic consumer to consume the message*/
            //QueueConsumer.Consumer(chanel);

            /*This is for direct exchange*/
            //DirectExchangeConsumer.Consumer(chanel);

            /*This is for Topic exchange*/
            //TopicExchangeConsumer.Consumer(chanel);

            /*This is for Header Exchange*/
            //HeaderExchangeConsumer.Consumer(chanel);

            /*This is for fan out exchange*/
            FanoutExchangeConsumer.Consumer(chanel);

            Console.ReadLine();

        }
    }
}
