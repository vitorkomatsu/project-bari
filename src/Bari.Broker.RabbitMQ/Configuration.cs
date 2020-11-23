using System;
using Microsoft.Extensions.Configuration;

namespace Barigui.Broker.RabbitMQ
{
    public class Configuration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public Configuration(IConfiguration configuration)
        {
            Host = configuration["RabbitMQ:Host"];
            Port = Convert.ToInt32(configuration["RabbitMQ:Port"]);
            User = configuration["RabbitMQ:User"];
            Password = configuration["RabbitMQ:Password"];
        }
    }
}
