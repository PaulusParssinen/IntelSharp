using System;
using System.Net;
using System.Web;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using IntelSharp.Json;
using IntelSharp.Model;

namespace IntelSharp
{
    public static class IXAPI
    {
        private const string USER_AGENT = "IntelSharp/1.0 (github.com/PaulusParssinen/IntelSharp)";

        private readonly static HttpClient _client;
        private readonly static JsonSerializerOptions _serializerOptions;

        static IXAPI()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);

            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new DateTimeConverter());
            _serializerOptions.PropertyNameCaseInsensitive = true;
        }

        public static Task<AuthenticationInfo> GetAuthenticationInfoAsync(IXApiContext context) => GetAsync<AuthenticationInfo>(context, "/authenticate/info");
        
        public static HttpRequestMessage CreateRequest(IXApiContext context, HttpMethod method, string path,
            object? parameters = default)
        {
            var request = new HttpRequestMessage(method, context.BaseUri.GetLeftPart(UriPartial.Authority) + path);

            if (!string.IsNullOrEmpty(context.Key))
                request.Headers.Add("X-Key", context.Key);

            if (parameters != default)
            {
                string jsonContent = JsonSerializer.Serialize(parameters, _serializerOptions);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }
            return request;
        }

        public static async Task<T> GetAsync<T>(string requestUrl,
            Func<HttpContent, Task<T>>? contentDeserializer = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }
        public static async Task<T> GetAsync<T>(IXApiContext context, string path,
            Dictionary<string, object>? queryParameters = default,
            Func<HttpContent, Task<T>>? contentDeserializer = default)
        {
            if (queryParameters?.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(string.Empty); //We need QueryBuilder
                foreach (var (key, value) in queryParameters)
                {
                    query.Add(key, value.ToString());
                }
                path += "?" + query.ToString();
            }

            using var request = CreateRequest(context, HttpMethod.Get, path);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> PostAsync<T>(IXApiContext context, string path,
            object? parameters = default,
            Func<HttpContent, Task<T>>? contentDeserializer = default)
        {
            using var request = CreateRequest(context, HttpMethod.Post, path, parameters);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage response,
            Func<HttpContent, Task<T>>? responseContentConverter = default)
        {
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => throw new InvalidOperationException("Invalid input. Encoding is invalid or a required parameter is missing."),
                    HttpStatusCode.Unauthorized => throw new UnauthorizedAccessException("Access not authorized. This may be due missing permission for API call or to selected buckets."),
                    HttpStatusCode.PaymentRequired => throw new UnauthorizedAccessException("No credits available."),
                    HttpStatusCode.NotFound => throw new Exception("Item or identifier not found."),

                    _ => default,
                };
            }

            if (responseContentConverter != null)
                return await responseContentConverter(response.Content).ConfigureAwait(false);

            if (typeof(T) == typeof(string))
                return (T)(object)await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (typeof(T) == typeof(byte[]))
                return (T)(object)await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (response.Content.Headers.ContentType.MediaType == "application/json")
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync().ConfigureAwait(false), _serializerOptions).ConfigureAwait(false);

            return default;
        }
    }
}
