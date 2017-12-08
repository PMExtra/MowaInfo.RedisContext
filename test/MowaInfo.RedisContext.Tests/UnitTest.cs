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
    }
}
