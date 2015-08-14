using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// Interface for a simple HTTP service.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <param name="path">The URI path.</param>
        /// <returns>The retrieved content or NULL in case of an error.</returns>
        Task<string> GetAsync(Uri path);

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PutAsync<T>(Uri path, T payload);

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PostAsync<T>(Uri path, T payload);

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        /// <param name="path">The URI path.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> DeleteAsync(Uri path);
    }
}
