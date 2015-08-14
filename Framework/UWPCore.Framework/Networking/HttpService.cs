using System;
using System.Linq;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UWPCore.Framework.Data;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace UWPCore.Framework.Networking
{
    /// <summary>
    /// The HTTP service class that provides simple Web API functionality.
    /// </summary>
    public class HttpService : IHttpService
    {
        /// <summary>
        /// The serialization service.
        /// </summary>
        private ISerializationService _serializationService;

        /// <summary>
        /// Gets or sets the used encoding.
        /// </summary>
        public UnicodeEncoding Encoding { get; set; } = UnicodeEncoding.Utf8;

        /// <summary>
        /// Creates a HttpService instance.
        /// </summary>
        public HttpService()
        {
            _serializationService = new DataContractSerializationService();
        }

        public async Task<string> GetAsync(Uri path)
        {
            using (var http = GetClient())
            {
                var response = await http.GetAsync(path);
                if (response.StatusCode == HttpStatusCode.Ok)
                    return await response.Content.ReadAsStringAsync();
                else
                    return null;
            }
        }

        public async Task<string> GetAsync(Uri path, NameValueCollection parameters)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(Uri path, T payload)
        {
            using (var http = GetClient())
            {
                var data = _serializationService.SerializeJson(payload);
                var content = new HttpStringContent(data, Encoding, HttpConstants.APPLICATION_JSON);
                return await http.PutAsync(path, content);
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(Uri path, NameValueCollection parameters, T payload)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await PutAsync(uri, payload);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(Uri path, T payload)
        {
            using (var http = GetClient())
            {
                var data = _serializationService.SerializeJson(payload);
                var content = new HttpStringContent(data, Encoding, HttpConstants.APPLICATION_JSON);
                return await http.PostAsync(path, content);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(Uri path, NameValueCollection parameters, T payload)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await PostAsync(uri, payload);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri path)
        {
            using (var http = GetClient())
            {
                return await http.DeleteAsync(path);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri path, NameValueCollection parameters)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await DeleteAsync(uri);
        }

        /// <summary>
        /// Gets a new HTTP client.
        /// </summary>
        /// <returns>The new HTTP client.</returns>
        private HttpClient GetClient()
        {
            var httpClient = new HttpClient();
            var header = new HttpMediaTypeWithQualityHeaderValue(HttpConstants.APPLICATION_JSON);
            httpClient.DefaultRequestHeaders.Accept.Add(header);
            return httpClient;
        }

        /// <summary>
        /// Converts the name value collection to an encoded query string.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The ecoded query string.</returns>
        private string ToEncodedQueryString(NameValueCollection parameters)
        {
            var array = (from key in parameters.AllKeys
                         from value in parameters.GetValues(key)
                         select string.Format("{0}={1}", Uri.EscapeUriString(key), Uri.EscapeUriString(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
