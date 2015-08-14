using System;
using System.Threading.Tasks;
using UWPCore.Framework.Data;
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
        /// The HTTP client.
        /// </summary>
        private HttpClient _httpClient;

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

        public async Task<HttpResponseMessage> PutAsync<T>(Uri path, T payload)
        {
            using (var http = GetClient())
            {
                var data = _serializationService.SerializeJson(payload);
                var content = new HttpStringContent(data, Windows.Storage.Streams.UnicodeEncoding.Utf8, HttpConstants.APPLICATION_JSON);
                return await http.PutAsync(path, content);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(Uri path, T payload)
        {
            using (var http = GetClient())
            {
                var data = _serializationService.SerializeJson(payload);
                var content = new HttpStringContent(data, Windows.Storage.Streams.UnicodeEncoding.Utf8, HttpConstants.APPLICATION_JSON);
                return await http.PostAsync(path, content);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri path)
        {
            using (var http = GetClient())
            {
                return await http.DeleteAsync(path);
            }
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
    }
}
