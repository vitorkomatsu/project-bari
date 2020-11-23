namespace Barigui.Domain.Core.Broker
{
    public interface IListener
    {
        void Start(IConsumer consumer, string exchangeName, string queueName);
        void Stop();
        string ConsumerTag { get; set; }
    }
}
