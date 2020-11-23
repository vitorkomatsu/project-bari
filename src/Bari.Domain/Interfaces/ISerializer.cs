using System;
using Barigui.Domain.Core.Broker;

namespace Barigui.Domain.Interfaces
{
    public interface ISerializer
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        byte[] Serialize<T>(T @object);

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        T Deserialize<T>(byte[] data);

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        IMessage Deserialize(Type type, byte[] data);
    }
}
