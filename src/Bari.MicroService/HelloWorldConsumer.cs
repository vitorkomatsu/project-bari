using Barigui.Broker.RabbitMQ;
using Barigui.Domain.Core.Broker;
using Barigui.Domain.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barigui.MicroService
{
    public class HelloWorldConsumer : Consumer<HelloWorldMessage>, IConsumer
    {
        private readonly ILogger _logger;
        private readonly Settings _settings;

        public HelloWorldConsumer(ILogger logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public void Consume(IMessage message)
        {
            var helloWorldMessage = (HelloWorldMessage)message;
            if (_settings.ServiceName != helloWorldMessage.ServiceName)
            {
                _logger.LogInformation($"{_settings.ServiceName} received new message from {helloWorldMessage.ServiceName}: {JsonConvert.SerializeObject(helloWorldMessage)}");
            }
        }
    }
}