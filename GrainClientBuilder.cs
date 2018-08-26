using System;
using Newtonsoft.Json;

namespace RedisPubSubTest
{
    internal class GrainClientBuilder<T> where T: class, IGrainIdentifierBase
    {
        RedisClientMy _client;
        T _message;
        public GrainClientBuilder(RedisClientMy client, T message)
        {
            _client = client;
            _message = message;
        }

        public void Send()
        {
            var message = new MessageBase()
            {
                MessageType = _message.GetType().FullName,
                Channel = CalcChannelName(_message),
                Body = JsonConvert.SerializeObject(_message)
            };

            var serializedMessage = JsonConvert.SerializeObject(message);
            _client.Publish(serializedMessage);
        }

        private string CalcChannelName(T message)
        {
            return _message.GetType().FullName + ";" + "ipaddress" + DateTime.Now.Ticks;
        }
    }
}