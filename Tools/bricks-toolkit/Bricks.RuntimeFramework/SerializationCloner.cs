using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bricks
{
    public class SerializationCloner
    {
        public static T Clone<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                return (T) formatter.Deserialize(stream);
            }
        }
    }
}