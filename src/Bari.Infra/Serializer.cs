using System;
using System.Text;
using Barigui.Domain.Core.Broker;
using Barigui.Domain.Interfaces;
using Newtonsoft.Json;

namespace Barigui.Infra
{
    public class Serializer : ISerializer
    {
        private readonly Encoding _encoding = Encoding.UTF8;

        public byte[] Serialize<T>(T data)
        {
            var body = JsonConvert.SerializeObject(data);

            return _encoding.GetBytes(body);
        }

        public T Deserialize<T>(byte[] data)
        {
            return JsonConvert.DeserializeObject<T>(_encoding.GetString(data));
        }

        public IMessage Deserialize(Type type, byte[] data)
        {
            return (IMessage)JsonConvert.DeserializeObject(_encoding.GetString(data), type);
        }
    }
}