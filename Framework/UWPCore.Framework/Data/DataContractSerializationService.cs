using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Serialization service to serialize and deserialize data using
    /// <see cref="DataContractSerializer"/> and <see cref="DataContractJsonSerializer"/>.
    /// </summary>
    public sealed class DataContractSerializationService : ISerializationService
    {
        #region String

        public void Serialize(Stream stream, string data)
        {
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(data);
            streamWriter.Flush();
        }

        public string Deserialize(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            string result = streamReader.ReadToEnd();
            return result;
        }

        #endregion

        #region XML

        public void SerializeXML<T>(Stream stream, T data)
        {
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, data);
        }

        public string SerializeXML<T>(T data)
        {
            using (var memStream = new MemoryStream())
            {
                SerializeXML<T>(memStream, data);

                memStream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(memStream))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        public T DeserializeXML<T>(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var result = (T)serializer.ReadObject(stream);
            return result;
        }

        public T DeserializeXML<T>(string data)
        {
            using (var memStream = new MemoryStream())
            {
                byte[] encodedData = System.Text.Encoding.UTF8.GetBytes(data);
                memStream.Write(encodedData, 0, encodedData.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return DeserializeXML<T>(memStream);
            }
        }

        #endregion

        #region JSON

        public void SerializeJson<T>(Stream stream, T data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, data);
        }

        public string SerializeJson<T>(T data)
        {
            using (var memStream = new MemoryStream())
            {
                SerializeJson<T>(memStream, data);

                memStream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(memStream))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        public T DeserializeJson<T>(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var result = (T)serializer.ReadObject(stream);
            return result;
        }

        public T DeserializeJson<T>(string data)
        {
            using (var memStream = new MemoryStream())
            {
                byte[] encodedData = System.Text.Encoding.UTF8.GetBytes(data);
                memStream.Write(encodedData, 0, encodedData.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return DeserializeJson<T>(memStream);
            }
        }

        #endregion
    }
}
