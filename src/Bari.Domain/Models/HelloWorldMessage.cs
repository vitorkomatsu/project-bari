using Barigui.Domain.Core.Broker;

namespace Barigui.Domain.Models
{
    public class HelloWorldMessage : Message
    {
        public string Description { get; set; }
        public string ServiceName { get; set; }
        public HelloWorldMessage(string serviceName)
        {
            this.Description = "Hello World!";
            this.ServiceName = serviceName;
        }

        public HelloWorldMessage() { } //Newtonsoft
    }
}
