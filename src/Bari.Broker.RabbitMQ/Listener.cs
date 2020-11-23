using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Barigui.Broker.RabbitMQ
{
    public class Listener : IListener
    {
        private readonly IModel _model;
        private readonly ISerializer _serializer;

        public string ConsumerTag { get; set; }
       
        public Listener(IModel model, ISerializer serializer)
        {
            _serializer = serializer;
            _model = model;
        }

        public void Start(IConsumer consumer, string exchangeName, string queueName)
        {
            _model.ExchangeDeclare(exchangeName, "fanout", true);

            _model.QueueDeclare(queueName, true, false, false);

            _model.BasicQos(0, 1, false);

            _model.QueueBind(queueName, exchangeName, "");

            var basicConsumer = new EventingBasicConsumer(_model);
            basicConsumer.Received += (channel, args) => OnReceived(consumer, args);

            ConsumerTag = _model.BasicConsume(queueName, false, basicConsumer);
        }

        public void OnReceived(IConsumer consumer, BasicDeliverEventArgs args)
        {
            var message = _serializer.Deserialize(consumer.GetMessageType(), args.Body);
            try
            {
                consumer.Consume(message);
                _model.BasicAck(args.DeliveryTag, false);
            }
            catch
            {
                _model.BasicNack(args.DeliveryTag, false, true);
            }
        }

        public void Stop()
        {
            _model.BasicCancel(ConsumerTag);
        }
    }
}
