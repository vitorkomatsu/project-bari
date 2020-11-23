using System;
using Barigui.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Barigui.Domain.Tests
{
    public class MessageTests
    {
        private const string ServiceName = "testeService";

        [Fact]
        public void CreateNewMessage_Description_ShouldBeHelloWorld()
        {
            var message = new HelloWorldMessage(ServiceName);

            message.Description.Should().BeEquivalentTo("Hello World!");
        }

        [Fact]
        public void CreateNewMessage_ServiceName_ShouldMatchConst()
        {
            var message = new HelloWorldMessage(ServiceName);

            message.ServiceName.Should().BeEquivalentTo(ServiceName);
        }

        [Fact]
        public void CreateNewMessage_Id_MustNotBeEmpty()
        {
            var message = new HelloWorldMessage(ServiceName);

            message.Id.Should().NotBeEmpty();
            message.Id.Should().NotBe(Guid.Empty);
        }
    }
}
