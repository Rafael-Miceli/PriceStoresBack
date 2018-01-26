using System.Threading.Tasks;
using System.Text;
using Api.ApplicationServices.Interfaces;
using Api.Models;
using RabbitMQ.Client;

namespace Api.MessageSender
{
    public class ProductNotifier: IWantToNotify
    {
        public async Task NewProduct(Product message) 
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
        
        public async Task UpdateProduct(Product message)
        {

        }
    }
}