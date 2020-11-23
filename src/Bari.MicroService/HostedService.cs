using System;
using System.Threading;
using System.Threading.Tasks;
using Barigui.Broker.RabbitMQ;
using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using Barigui.Domain.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Barigui.MicroService
{
    public class HostedService : IHostedService
    {
        private readonly Settings _settings;
        private readonly IModel _model;
        private readonly ISerializer _serializer;
        private readonly string QueueName = "barigui";
        private readonly ILogger _logger;
        private readonly IListener _listener;

        public HostedService(Settings settings, IModel model, ISerializer serializer, ILogger<HostedService> logger)
        {
            _settings = settings;
            _model = model;
            _serializer = serializer;
            _logger = logger;
            _listener = new Listener(model, _serializer);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var publisher = new Publisher(_model, _serializer);

            _listener.Start(new HelloWorldConsumer(_logger, _settings), $"{QueueName}.fanout", $"{QueueName}.{_settings.ServiceName}");

            while (true)
            {
                _logger.LogDebug($"{_settings.ServiceName} - Sending new message to broker");
                publisher.Publish(new HelloWorldMessage(_settings.ServiceName), QueueName);
                _logger.LogDebug($"{_settings.ServiceName} - waiting delay {_settings.Delay} to queue new message");
                await Task.Delay(_settings.Delay, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _listener.Stop();

            return Task.CompletedTask;
        }
    }
}
