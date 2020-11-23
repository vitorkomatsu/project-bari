using Barigui.Broker.RabbitMQ;
using Barigui.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Barigui.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration = new Configuration(configuration);
            services.AddSingleton(s => rabbitMqConfiguration);
            services.AddSingleton<ISerializer, Serializer>();
            services.AddSingleton<IModel>(s => new Connection(rabbitMqConfiguration).CreateConnection().CreateModel());
        }
    }
}
