using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System;
using Api.ViewModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace tests.Integration
{
    [TestClass]
    public class NewProductQueueTests
    {
        //private string productQueueConnection = "tcp://localhost:25672";

        [TestMethod]
        public void Smoke_Connection_To_Queue()
        {
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

            
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: "newproduct", type: "fanout");

                var message = "Cebola";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "newproduct",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);
            }
        }
        

        [TestMethod]
        public void Smoke_Read_From_Queue()
        {
            // var factory = new ConnectionFactory() { HostName = "localhost" };
            // using (var connection = factory.CreateConnection())
            // using (var channel = connection.CreateModel())
            // {
            //     channel.QueueDeclare(queue: "hello",
            //                          durable: true,
            //                          exclusive: false,
            //                          autoDelete: false,
            //                          arguments: null);

            //     var consumer = new EventingBasicConsumer(channel);
            //     consumer.Received += (model, ea) =>
            //     {
            //         var body = ea.Body;
            //         var message = Encoding.UTF8.GetString(body);
            //         Console.WriteLine(" [x] Received {0}", message);

            //         channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //     };
            //     channel.BasicConsume(queue: "hello",
            //                          autoAck: false,
            //                          consumer: consumer);

            //     Console.WriteLine(" Press [enter] to exit.");
            //     Console.ReadLine();
            // }


            //Ler de exchange
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                exchange: "logs",
                                routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                    autoAck: true,
                                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }

}