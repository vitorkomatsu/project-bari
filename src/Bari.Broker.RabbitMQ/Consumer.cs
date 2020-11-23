using System;

namespace Barigui.Broker.RabbitMQ
{
    public class Consumer<T>
    {
        public Type GetMessageType()
        {
            return typeof(T);
        }
    }
}
