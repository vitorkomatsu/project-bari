using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace Barigui.Broker.RabbitMQ.Tests
{
    public class PublisherTests
    {
        private Mock<IModel> _mockModel;
        private readonly Mock<ISerializer> _mockSerializer;
        private readonly Mock<IMessage> _mockMessage;

        private readonly IPublisher _publisher;

        private const string queueName = "barigui";

        public PublisherTests()
        {
            SetupModel();

            _mockSerializer = new Mock<ISerializer>();
            _mockMessage = new Mock<IMessage>();

            _publisher = new Publisher(_mockModel.Object, _mockSerializer.Object);
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
        public void PublishNewMessage_ShouldCall_ModelBasicPublish()
        {
            _publisher.Publish(_mockMessage.Object, queueName);
            _mockModel.Verify(v => v.BasicPublish($"{queueName}.fanout", string.Empty, false, It.IsAny<IBasicProperties>(), new byte[] { }), Times.Once);
        }

        [Fact]
        public void PublishNewMessage_ShouldCall_SerializerSerialize()
        {
            _publisher.Publish(_mockMessage.Object, queueName);
            _mockSerializer.Verify(v => v.Serialize(It.IsAny<IMessage>()), Times.Once);
        }
    }
}
