using System;

namespace MowaInfo.RedisContext.Annotations
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
