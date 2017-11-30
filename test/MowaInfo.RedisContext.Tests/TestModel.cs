namespace MowaInfo.RedisContext.Tests
{
    public class TestModel
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool ValueEquals(TestModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Address, other.Address);
        }
    }
}
