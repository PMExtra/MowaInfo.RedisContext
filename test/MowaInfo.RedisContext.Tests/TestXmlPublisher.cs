using System.Threading.Tasks;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestXmlPublisher : RedisPublisher
    {
        public TestXmlPublisher(RedisChannel channel)
        {
            Channel = channel;
        }

        public long Publish(TestModel message, CommandFlags flags = CommandFlags.None)
        {
            return base.Publish(XmlHelper.ToXmlString(message), flags);
        }

        public Task<long> PublishAsync(TestModel message, CommandFlags flags = CommandFlags.None)
        {
            return base.PublishAsync(XmlHelper.ToXmlString(message), flags);
        }
    }
}
