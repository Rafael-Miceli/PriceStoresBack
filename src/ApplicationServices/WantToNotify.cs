using Api.ApplicationServices.Interfaces;
using Api.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Api.ApplicationServices
{
    public class WantToNotify : IWantToNotify
    {
        public void NewProduct(Product message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: "newproduct", type: "fanout");

                var body = Encoding.UTF8.GetBytes(message.Name);

                channel.BasicPublish(exchange: "newproduct",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);
            }
        }

        public void UpdateProduct(Product message)
        {
            throw new System.NotImplementedException();
        }
    }
}