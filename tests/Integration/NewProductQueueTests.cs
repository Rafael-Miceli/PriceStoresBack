using Api.ApplicationServices;
using Api.Data;
using Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System;
using Api.ViewModels;
using RabbitMQ.Client;
using System.Text;

namespace tests.Integration
{
    [TestClass]
    public class NewProductQueueTests
    {
        private string productQueueConnection = "tcp://localhost:25672";

        [TestMethod]
        public void Smoke_Connection_To_Queue()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                        routingKey: "hello",
                                        basicProperties: null,
                                        body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }

}