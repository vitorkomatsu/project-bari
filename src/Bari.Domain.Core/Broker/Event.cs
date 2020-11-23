using System;

namespace Barigui.Domain.Core.Broker
{
    public class Message : IMessage
    {
        private DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }

        protected Message()
        {
            this.CreatedAt = DateTime.Now;
            this.Id = Guid.NewGuid();
        }
    }
}
