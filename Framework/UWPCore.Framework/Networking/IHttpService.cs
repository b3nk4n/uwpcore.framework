using System;
using System.Collections.Generic;
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
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved content or NULL in case of an error.</returns>
        Task<HttpResponseMessage> GetAsync(Uri path, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <param name="path">The URI path.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved content or NULL in case of an error.</returns>
        Task<HttpResponseMessage> GetAsync(Uri path, Dictionary<string, object> parameters, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PutAsync<T>(Uri path, T payload, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PutAsync<T>(Uri path, Dictionary<string, object> parameters, T payload, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="content">The HTTP content to send, such as <see cref="HttpMultipartFormDataContent"/> or <see cref="HttpStringContent"/>.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PostAsync(Uri path, IHttpContent content, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PostJsonAsync<T>(Uri path, T payload, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <typeparam name="T">The payload type that must be serializable.</typeparam>
        /// <param name="path">The URI path.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="payload">The serializable payload data.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> PostJsonAsync<T>(Uri path, Dictionary<string, object> parameters, T payload, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        /// <param name="path">The URI path.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> DeleteAsync(Uri path, int? timeoutMillis = null, string token = null);

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        /// <param name="path">The URI path.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <param name="timeoutMillis">The optional timeout in millis.</param>
        /// <returns>The retrieved response or NULL in case of an error.</returns>
        Task<HttpResponseMessage> DeleteAsync(Uri path, Dictionary<string, object> parameters, int? timeoutMillis = null, string token = null);
    }
}
