using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MowaInfo.RedisContext.Tests
{
    public static class XmlHelper
    {
        public static string ToXmlString(object entity)
        {
            using (var stream = new MemoryStream())
            {
#if NETCOREAPP1_1
                var writer = XmlWriter.Create(stream, new XmlWriterSettings { Encoding = Encoding.UTF8 });
#else
                var writer = new XmlTextWriter(stream, Encoding.UTF8);
#endif
                var xml = new XmlSerializer(entity.GetType());
                xml.Serialize(writer, entity);
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    stream.Position = 0;
                    var xmlString = sr.ReadToEnd();
                    return xmlString;
                }
            }
        }

        public static T ParseFromXml<T>(string xml)
        {
            var bytes = Encoding.UTF8.GetBytes(xml);
            using (var stream = new MemoryStream(bytes))
            {
                var xs = new XmlSerializer(typeof(T));
                var entity = (T)xs.Deserialize(stream);
                return entity;
            }
        }
    }
}
