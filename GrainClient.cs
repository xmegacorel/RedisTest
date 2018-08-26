namespace RedisPubSubTest
{
    public class GrainClient
    {
        RedisClientMy _client;
        public GrainClient(RedisClientMy client)
        {
            _client = client;
        }
         
        public GrainClientBuilder<T> AddMessage<T>(T message) where T: class, IGrainIdentifierBase
        {
            return null;
        }
    }
}