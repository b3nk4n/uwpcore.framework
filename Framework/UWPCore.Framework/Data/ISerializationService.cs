using System.IO;

namespace UWPCore.Framework.Data
{
    /// <summary>
    /// Interface for serialization service.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Serialize string to stream as text.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        void Serialize(Stream stream, string data);

        /// <summary>
        /// Deserialize text from stream to string.
        /// </summary>
        /// <param name="stream">Source stream.</param>
        /// <returns>Data as string.</returns>
        string Deserialize(Stream stream);

        /// <summary>
        /// Serialize object to stream as xml.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        void SerializeXML<T>(Stream stream, T data);

        /// <summary>
        /// Serialize object as xml.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="data">Data to serialize.</param>
        /// <returns>The serialized data or NULL in case of an error.</returns>
        string SerializeXML<T>(T data);

        /// <summary>
        /// Deserialize xml from stream to object.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Source stream.</param>
        /// <returns>Data as object.</returns>
        T DeserializeXML<T>(Stream stream);

        /// <summary>
        /// Deserialize xml to object.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="data">Data to serialize.</param>
        /// <returns>The deserialized object or NULL in case of an error.</returns>
        T DeserializeXML<T>(string data);

        /// <summary>
        /// Serialize object to stream as json.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        void SerializeJson<T>(Stream stream, T data);

        /// <summary>
        /// Serialize object as json.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="data">The data to serialize.</param>
        /// <returns>The serialized data or NULL in case of an error.</returns>
        string SerializeJson<T>(T data);

        /// <summary>
        /// Deserialize xml from stream.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Source stream.</param>
        /// <returns>Data as object.</returns>

        T DeserializeJson<T>(Stream stream);

        /// <summary>
        /// Deserialize xml to object.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="data">The data to deserialize.</param>
        /// <returns>The deserialized object or NULL in case of an error.</returns>
        T DeserializeJson<T>(string data);
    }

}
