using System;
using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Xunit;

namespace Barigui.Broker.RabbitMQ.Tests
{
    public class ListenerTests : IDisposable
    {
        private Mock<IModel> _mockModel;
        private readonly Mock<ISerializer> _mockSerializer;
        private readonly Mock<IConsumer> _mockConsumer;

        private readonly Listener _listener;
        private readonly BasicDeliverEventArgs _basicDeliverEventArgs;

        private const string queueName = "barigui";
        private const string exchangeName = "barigui.fanout";

        public ListenerTests()
        {
            SetupModel();

            _mockSerializer = new Mock<ISerializer>();
            _mockConsumer = new Mock<IConsumer>();

            _listener = new Listener(_mockModel.Object, _mockSerializer.Object);
            _basicDeliverEventArgs = new BasicDeliverEventArgs(_listener.ConsumerTag, 1, false, exchangeName, string.Empty, Properties().Object, new byte[] { });
        }

        private void SetupModel()
        {
            var basicProperties = Properties();

            _mockModel = new Mock<IModel>();
            _mockModel.Setup(s => s.CreateBasicProperties()).Returns(basicProperties.Object);
        }

        private Mock<IBasicProperties> Properties()
        {
            var props = new Mock<IBasicProperties>();
            props.SetupProperty(p => p.Persistent, false);
            props.SetupProperty(p => p.DeliveryMode);
            props.SetupProperty(p => p.ContentEncoding, null);
            return props;
        }

        [Fact]
        public void StartListening_ShouldCall_ModelExchangeDeclare()
        {
            _listener.Start(_mockConsumer.Object, exchangeName, queueName);
            _mockModel.Verify(v => v.ExchangeDeclare(exchangeName, "fanout", true, false, null), Times.Once);
        }

        [Fact]
        public void StartListeningAndReceivedAMessage_ShouldCall_OnReceived()
        {
            _listener.Start(_mockConsumer.Object, exchangeName, queueName);

            _listener.OnReceived(_mockConsumer.Object, _basicDeliverEventArgs);

            _listener.Stop();
        }

        [Fact]
        public void StopListening_ShouldCall_ModelBasicCancel()
        {
            _listener.Start(_mockConsumer.Object, exchangeName, queueName);

            _listener.Stop();

            _mockModel.Verify(v => v.BasicCancel(_listener.ConsumerTag), Times.Once);
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}
