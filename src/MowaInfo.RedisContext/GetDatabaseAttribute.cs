using System;

namespace MowaInfo.RedisContext
{
    public class GetDatabaseAttribute : Attribute
    {
        public GetDatabaseAttribute(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
