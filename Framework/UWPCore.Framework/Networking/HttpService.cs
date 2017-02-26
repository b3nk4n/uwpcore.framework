using System;
using System.Linq;
using System.Threading.Tasks;
using UWPCore.Framework.Data;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Ninject;
using System.Threading;
using System.Collections.Generic;
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
        [Inject]
        public HttpService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri path, int? timeoutMillis = null, string token = null)
        {
            try
            {
                using (var http = GetClient(token))
                {
                    if (timeoutMillis != null)
                    {
                        var cancelation = new CancellationTokenSource(timeoutMillis.Value);
                        return await http.GetAsync(path).AsTask(cancelation.Token);
                    }
                    else
                    {
                        return await http.GetAsync(path);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // timeout
                return null;
            }
            catch (Exception)
            {
                // server error, offline, ...
                return null;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(Uri path, Dictionary<string, object> parameters, int? timeoutMillis = null, string token = null)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await GetAsync(uri, timeoutMillis);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(Uri path, T payload, int? timeoutMillis = null, string token = null)
        {
            try
            {
                using (var http = GetClient(token))
                {
                    var data = _serializationService.SerializeJson(payload);
                    var content = new HttpStringContent(data, Encoding, HttpConstants.APPLICATION_JSON);
                    if (timeoutMillis != null)
                    {
                        var cancelation = new CancellationTokenSource(timeoutMillis.Value);
                        return await http.PutAsync(path, content).AsTask(cancelation.Token);
                    }
                    else
                    {
                        return await http.PutAsync(path, content);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // timeout
                return null;
            }
            catch (Exception)
            {
                // server error, offline, ...
                return null;
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(Uri path, Dictionary<string, object> parameters, T payload, int? timeoutMillis = null, string token = null)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await PutAsync(uri, payload, timeoutMillis, token);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri path, IHttpContent content, int? timeoutMillis = null, string token = null)
        {
            try
            {
                using (var http = GetClient(token))
                {
                    if (timeoutMillis != null)
                    {
                        var cancelation = new CancellationTokenSource(timeoutMillis.Value);
                        return await http.PostAsync(path, content).AsTask(cancelation.Token);
                    }
                    else
                    {
                        return await http.PostAsync(path, content);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // timeout
                return null;
            }
            catch (Exception)
            {
                // server error, offline, ...
                return null;
            }
        }

        public async Task<HttpResponseMessage> PostJsonAsync<T>(Uri path, T payload, int? timeoutMillis = null, string token = null)
        {
            var data = _serializationService.SerializeJson(payload);
            var content = new HttpStringContent(data, Encoding, HttpConstants.APPLICATION_JSON);
            return await PostAsync(path, content, timeoutMillis, token);
        }

        public async Task<HttpResponseMessage> PostJsonAsync<T>(Uri path, Dictionary<string, object> parameters, T payload, int? timeoutMillis = null, string token = null)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await PostJsonAsync(uri, payload, timeoutMillis, token);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri path, int? timeoutMillis = null, string token = null)
        {
            try
            {
                using (var http = GetClient(token))
                {
                    if (timeoutMillis != null)
                    {
                        var cancelation = new CancellationTokenSource(timeoutMillis.Value);
                        return await http.DeleteAsync(path).AsTask(cancelation.Token);
                    }
                    else
                    {
                        return await http.DeleteAsync(path);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // timeout
                return null;
            }
            catch (Exception)
            {
                // server error, offline, ...
                return null;
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri path, Dictionary<string, object> parameters, int? timeoutMillis = null, string token = null)
        {
            var uri = new Uri(path.AbsolutePath + ToEncodedQueryString(parameters));
            return await DeleteAsync(uri, timeoutMillis, token);
        }

        /// <summary>
        /// Gets a new HTTP client.
        /// </summary>
        /// <returns>The new HTTP client.</returns>
        private HttpClient GetClient(string token = null)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue(HttpConstants.APPLICATION_JSON));
            if (token != null)
                httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", token);
            return httpClient;
        }

        /// <summary>
        /// Converts the name value collection to an encoded query string.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The ecoded query string.</returns>
        private string ToEncodedQueryString(Dictionary<string, object> parameters)
        {
            var array = (from key in parameters.Keys
                         select string.Format("{0}={1}", Uri.EscapeUriString(key), Uri.EscapeUriString(parameters[key].ToString())))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}

