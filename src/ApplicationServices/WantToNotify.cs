using Api.ApplicationServices.Interfaces;
using Api.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Api.ApplicationServices
{
    public class WantToNotify : IWantToNotify
    {
        public void NewProduct(Product message)
        {
            // var message = GetMessage(args);
            // var body = Encoding.UTF8.GetBytes(message);
            // channel.BasicPublish(exchange: "newproduct",
            //                     routingKey: "",
            //                     basicProperties: null,
            //                     body: body);

            // var factory = new ConnectionFactory() { HostName = "localhost" };
            // using (var connection = factory.CreateConnection())
            // using (var channel = connection.CreateModel())
            // {
            //     channel.QueueDeclare(queue: "hello",
            //                     durable: true,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            //     string message = "Hello World!";
            //     var body = Encoding.UTF8.GetBytes(message);

            //     channel.BasicPublish(exchange: "",
            //                         routingKey: "hello",
            //                         basicProperties: null,
            //                         body: body);
            //     Console.WriteLine(" [x] Sent {0}", message);
            // }
        }

        public void UpdateProduct(Product message)
        {
            throw new System.NotImplementedException();
        }
    }
}