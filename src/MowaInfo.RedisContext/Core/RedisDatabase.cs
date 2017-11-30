using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisDatabase
    {
        private RedisContext _context;
        private int _db;

        protected IDatabase Database => _context.Connection.GetDatabase(_db);

        internal void Init(RedisContext context, int db)
        {
            _context = context;
            _db = db;
        }
    }
}
