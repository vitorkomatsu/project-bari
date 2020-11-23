namespace Barigui.Domain.Core.Broker
{
    public interface IPublisher
    {
        void Publish(IMessage message, string queueName);
    }
}
