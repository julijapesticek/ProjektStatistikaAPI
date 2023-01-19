using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ.Client;

namespace ProjektStatistikaAPI
{
    public class MyRabbitMq
    {
        private string _message;

        public MyRabbitMq(string message)
        {
            _message = message;
            CreateRabbitMQObject();
        }

        public void CreateRabbitMQObject()
        {
            var factory = new ConnectionFactory();

            factory.UserName = "student";
            factory.Password = "student123";
            factory.HostName = "studentdocker.informatika.uni-mb.si";
            factory.Port = 5672;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "iir-rv1-3", type: ExchangeType.Direct, durable: true);

                var messageProperty = channel.CreateBasicProperties();
                messageProperty.Headers = new Dictionary<string, object>();                          ;
                messageProperty.CorrelationId = "123";
                messageProperty.Type = "Info";
                messageProperty.Headers.Add("timestamp", DateTime.Now.ToString());
                messageProperty.Headers.Add("url", "http://studentdocker.informatika.uni-mb.si:10001/statistika");
                messageProperty.Headers.Add("imeAplikacije", "StatistikaAPI");

                var body = Encoding.UTF8.GetBytes(_message);
                channel.BasicPublish(exchange: "iir-rv1-3",
                                     routingKey: "",
                                     basicProperties: messageProperty,
                                     body: body);
            }
        }
    }
}
