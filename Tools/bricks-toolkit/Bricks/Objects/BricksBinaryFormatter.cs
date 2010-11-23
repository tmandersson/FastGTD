using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bricks.Objects
{
    public class BricksBinaryFormatter
    {
        public virtual byte[] ToByteArray(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public virtual object ToObject(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
                return new BinaryFormatter().Deserialize(stream);
        }
    }
}