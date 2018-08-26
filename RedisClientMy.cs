using System;
using ServiceStack.Redis;

namespace RedisPubSubTest
{
    public class RedisClientMy
    {
        private RedisClient _redisPublisher;
        private Uri _serverAddress;
        public RedisClientMy(Uri serverAddress)
        {
            _serverAddress = serverAddress;
        }

        public void Start()
        {
            _redisPublisher = new RedisClient(_serverAddress);
        }

        public void Publish(string message)
        {
            _redisPublisher.PublishMessage(Consts.CommandChannel, message);
        }
    }
}