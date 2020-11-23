using System;
using System.Collections.Generic;
using System.Text;
using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using Moq;
using RabbitMQ.Client;

namespace Barigui.Broker.RabbitMQ.Tests
{
    public class ConsumerTests
    {
        private Mock<IModel> _mockModel;
        private readonly Mock<ISerializer> _mockSerializer;
        private readonly Mock<IMessage> _mockMessage;

        private readonly IConsumer _consumer;

        private const string queueName = "barigui";

        public ConsumerTests()
        {

            //_consumer = new Consumer<HelloMessage>();
        }

        private class HelloMessage : IMessage
        {
            public Guid Id { get; set; }
        }
    }
}
