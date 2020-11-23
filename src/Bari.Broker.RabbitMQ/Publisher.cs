using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using RabbitMQ.Client;

namespace Barigui.Broker.RabbitMQ
{
    public class Publisher : IPublisher
    {
        private readonly IModel _model;
        private readonly ISerializer _serializer;

        public Publisher(IModel model, ISerializer serializer)
        {
            _serializer = serializer;
            _model = model;
        }

        public void Publish(IMessage message, string queueName)
        {
            _model.BasicPublish($"{queueName}.fanout", string.Empty, false, GetBasicProperties(), _serializer.Serialize(message));
        }

        private IBasicProperties GetBasicProperties()
        {
            var props = _model.CreateBasicProperties();

            props.Persistent = true;
            props.DeliveryMode = 2;
            props.ContentEncoding = "utf-8";

            return props;
        }
    }
}
