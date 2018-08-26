using System;
using System.Threading;
using ServiceStack.Redis;

namespace RedisPubSubTest
{
    class Program
    {
        public static int PublishMessageCount = 10;
        public static string MessagePrefix = "123";
        public static string ChannelName = "Channel1";
        static void Main(string[] args)
        {
            var messagesReceived = 0;


            using (var redisConsumer = new RedisClient("127.0.0.1:6379"))
            using (var subscription = redisConsumer.CreateSubscription())
            {
                subscription.OnSubscribe = channel =>
                {
                    Console.WriteLine("Subscribed to '{0}'", channel);
                };
                subscription.OnUnSubscribe = channel =>
                {
                    Console.WriteLine("UnSubscribed from '{0}'", channel);
                };
                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine("Received '{0}' from channel '{1}'", msg, channel);

                    //As soon as we've received all 5 messages, disconnect by unsubscribing to all channels
                    if (++messagesReceived == PublishMessageCount)
                    {
                        subscription.UnSubscribeFromAllChannels();
                    }
                };

                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Begin publishing messages...");

                    using (var redisPublisher = new RedisClient("127.0.0.1:6379"))
                    {
                        for (var i = 1; i <= PublishMessageCount; i++)
                        {
                            var message = MessagePrefix + i;
                            Console.WriteLine("Publishing '{0}' to '{1}'", message, ChannelName);
                            redisPublisher.PublishMessage(ChannelName, message);
                        }
                    }
                });

                Console.WriteLine("Started Listening On '{0}'", ChannelName);
                subscription.SubscribeToChannels(ChannelName); //blocking
            }

            Console.WriteLine("EOF");
        }
    }
}
