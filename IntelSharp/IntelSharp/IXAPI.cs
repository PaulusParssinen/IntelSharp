using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using IntelSharp.Json;

namespace IntelSharp
{
    public static class IXAPI
    {
        private readonly static HttpClient _client;
        private readonly static JsonSerializerOptions _serializerOptions;

        static IXAPI()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "IntelSharp/V1");

            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new DateTimeConverter());
        }

        public static HttpRequestMessage CreateRequest(IXApiContext context, HttpMethod method, string path,
            object parameters = default)
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

        public static async Task<HttpResponseMessage> SendAsync(this HttpRequestMessage request)
        {
            return await _client.SendAsync(request).ConfigureAwait(false);
        }

        public static async Task<T> GetAsync<T>(string requestUrl,
            Func<HttpContent, Task<T>> contentDeserializer = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }
        public static async Task<T> GetAsync<T>(IXApiContext context, string path,
            Func<HttpContent, Task<T>> contentDeserializer = null)
        {
            using var request = CreateRequest(context, HttpMethod.Get, path, null);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> PostAsync<T>(IXApiContext context, string path,
            object parameters = default,
            Func<HttpContent, Task<T>> contentDeserializer = null)
        {
            using var request = CreateRequest(context, HttpMethod.Post, path, parameters);
            using var response = await _client.SendAsync(request).ConfigureAwait(false);

            return await DeserializeContentAsync(response, contentDeserializer).ConfigureAwait(false);
        }

        public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage response,
            Func<HttpContent, Task<T>> responseContentConverter = null)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidOperationException("Invalid input. Encoding is invalid or a required parameter is missing");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Access not authorized. This may be due missing permission for API call or to selected buckets");

                if (response.StatusCode == HttpStatusCode.PaymentRequired)
                    throw new UnauthorizedAccessException("No credits available");

                return default;
            }

            if (responseContentConverter != null)
                return await responseContentConverter(response.Content).ConfigureAwait(false);

            if (typeof(T) == typeof(string))
                return (T)(object)await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (typeof(T) == typeof(byte[]))
                return (T)(object)await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            if (response.Content.Headers.ContentType.MediaType == "application/json")
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync().ConfigureAwait(false));

            return default;
        }
    }
}
