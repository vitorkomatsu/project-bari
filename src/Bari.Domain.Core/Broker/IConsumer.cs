using System;

namespace Barigui.Domain.Core.Broker
{
    public interface IConsumer
    {
        void Consume(IMessage message);
        Type GetMessageType();
    }
}
