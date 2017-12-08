using System.Threading.Tasks;
using Xunit;

namespace MowaInfo.RedisContext.Tests
{
    public class UnitTest
    {
        private readonly TestRedisContext _context = new TestRedisContext("localhost");

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
