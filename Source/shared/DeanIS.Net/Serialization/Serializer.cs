using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DeanIS.Net.Serialization
{
    public class Serializer<T>
        where T : class
    {
        public string ToString(T value)
        {
            var serializer = new DataContractSerializer(typeof(T), null, Int32.MaxValue, true, false, null);

            var stringWriter = new StringWriter();

            using (var writer = new XmlTextWriter(stringWriter))
            {
                serializer.WriteObject(writer, value);
            }

            return stringWriter.ToString(); 
        }

        public T FromString(string value)
        {
            if (value.Length <= 0) throw new ArgumentOutOfRangeException("value", "must have something to deserialize");

            var serializer = new DataContractSerializer(typeof(T));

            using (var reader = new XmlTextReader(new StringReader(value)))
            {
                return (T) serializer.ReadObject(reader);
            }
        }
    }
}
