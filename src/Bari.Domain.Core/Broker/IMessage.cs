using System;

namespace Barigui.Domain.Core.Broker
{
    public interface IMessage
    {
        Guid Id { get; set; }
    }
}
