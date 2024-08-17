using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientSamokat.LongPolling
{
    public class GetService
    {

        private HttpClient? _httpClient { get; set; }

        private JsonSerializerOptions? _serializerOptions { get; set; }


        public GetService()
        {
            _serializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
               
            };
        }

        public async Task<T> Get<T>(Action<HttpClient> HttpClientOptions)
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);
                HttpRequestMessage request = new(HttpMethod.Get, _httpClient.BaseAddress);
                using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                string jsonData = await response.Content.ReadAsStringAsync();

                T? data = JsonSerializer.Deserialize<T>(jsonData, _serializerOptions);
                return data;
            }
        }

        public async Task<byte[]> GetByteContent(Action<HttpClient> HttpClientOptions)
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);
                HttpRequestMessage request = new(HttpMethod.Get, _httpClient.BaseAddress);
                using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                byte[] byteData = await response.Content.ReadAsByteArrayAsync();

                return byteData;
            }
        }
    }
}
