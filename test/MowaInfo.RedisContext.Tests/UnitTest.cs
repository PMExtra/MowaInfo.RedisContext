using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace MowaInfo.RedisContext.Tests
{
    public class UnitTest
    {
        private static readonly HostString Host = new HostString("127.0.0.1");

        private readonly TestRedisContext _context = new TestRedisContext(Host);

        [Fact]
        public async Task TestDatabase()
        {
            var result = await _context.TestRedisDatabase.TestSetAsync("Tests", "Test Success");
            Assert.True(result);
            var value = await _context.TestRedisDatabase.TestGetAsync("Tests");
            Assert.Equal("Test Success", value);
        }

        [Fact]
        public async Task TestJsonSubscriber()
        {
            var sucess = new TaskCompletionSource<bool>();

            var testModel = new TestModel { Id = 1, Name = "name", Address = "123456" };

            var jsonObserver = new TestJsonObserver("TestJson", (channel, model) =>
            {
                Assert.True(testModel.ValueEquals(model));
                Assert.False(sucess.Task.IsCompleted);
                sucess.SetResult(true);
            });
            var jsonPublisher = new TestJsonPublisher("TestJson");

            _context.AddObserver(jsonObserver);
            _context.AddPublisher(jsonPublisher);

            var count = await jsonPublisher.PublishAsync(testModel);
            Assert.Equal(1, count);

            sucess.Task.Wait(TimeSpan.FromSeconds(1));
            Assert.True(sucess.Task.IsCompleted);
            Assert.True(sucess.Task.Result);

            jsonObserver.Dispose();

            await jsonPublisher.PublishAsync(new TestModel());

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task TestSimpleSubscriber()
        {
            var sucess = new TaskCompletionSource<bool>();
            var simpleObserver = new SimpleObserver("TestSimple", (channel, redisValue) =>
            {
                Assert.Equal("magic", redisValue);
                Assert.False(sucess.Task.IsCompleted);
                sucess.SetResult(true);
            });

            var simplePublisher = new SimplePublisher("TestSimple");
            _context.AddObserver(simpleObserver);
            _context.AddPublisher(simplePublisher);
            var count = await simplePublisher.PublishAsync("magic");
            Assert.Equal(1, count);

            sucess.Task.Wait(TimeSpan.FromSeconds(1));
            Assert.True(sucess.Task.IsCompleted);
            Assert.True(sucess.Task.Result);

            simpleObserver.Dispose();

            await simplePublisher.PublishAsync("boom!");

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task TestXmlSubscriber()
        {
            var sucess = new TaskCompletionSource<bool>();

            var testModel = new TestModel { Id = 1, Name = "name", Address = "123456" };

            var xmlObserver = new TestXmlObserver("TestXml", (channel, model) =>
            {
                Assert.True(testModel.ValueEquals(model));
                Assert.False(sucess.Task.IsCompleted);
                sucess.SetResult(true);
            });
            var xmlPublisher = new TestXmlPublisher("TestXml");

            _context.AddObserver(xmlObserver);
            _context.AddPublisher(xmlPublisher);

            var count = await xmlPublisher.PublishAsync(testModel);
            Assert.Equal(1, count);

            sucess.Task.Wait(TimeSpan.FromSeconds(1));
            Assert.True(sucess.Task.IsCompleted);
            Assert.True(sucess.Task.Result);

            xmlObserver.Dispose();

            await xmlPublisher.PublishAsync(new TestModel());

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
