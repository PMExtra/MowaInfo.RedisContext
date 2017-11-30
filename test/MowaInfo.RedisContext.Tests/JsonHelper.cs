using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MowaInfo.RedisContext.Tests
{
    public static class JsonHelper
    {
        public static string ToJsonString(object obj)
        {
            var json = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                json.WriteObject(ms, obj);
                var szJson = Encoding.UTF8.GetString(ms.ToArray());
                return szJson;
            }
        }

        public static T ParseFromJson<T>(string szJson)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                var dcj = new DataContractJsonSerializer(typeof(T));
                return (T)dcj.ReadObject(ms);
            }
        }
    }
}
