using System;
using RabbitMQ.Client;

namespace Barigui.Broker.RabbitMQ
{
    public class Connection : ConnectionFactory
    {
        public Connection(Configuration configuration)
        {
            HostName = configuration.Host;
            Port = configuration.Port;
            UserName = configuration.User;
            Password = configuration.Password;
            AutomaticRecoveryEnabled = true;
            HandshakeContinuationTimeout = TimeSpan.FromSeconds(30);
        }
    }
}
