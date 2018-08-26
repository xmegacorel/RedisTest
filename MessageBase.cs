namespace RedisPubSubTest
{
    public class MessageBase
    {
        public string MessageType { get;set; }
        public string Channel { get; set; }
        public string Body { get;set; }
    }
}