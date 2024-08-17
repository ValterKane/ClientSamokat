using ClientSamokat.Model;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientSamokat.LongPolling
{
    public class PutService
    {
        private HttpClient _httpClient { get; set; } = null!;
        private JsonSerializerOptions? _serializerOptions { get; set; } = null;

        public PutService()
        {
            _serializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,

            };
        }


        public async Task<bool> Put(Action<HttpClient> HttpClientOptions)
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);

                using HttpResponseMessage request = await _httpClient.PutAsync(_httpClient.BaseAddress, null);
                if (request.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }


        public async Task<bool> Put<T>(Action<HttpClient> HttpClientOptions, T ObjectToPut) where T : ObservableModel
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);

                string jObj = JsonSerializer.Serialize<T>(ObjectToPut, _serializerOptions);
                StringContent content = new(jObj, Encoding.UTF8, "application/json");
                using HttpResponseMessage request = await _httpClient.PutAsync(_httpClient.BaseAddress, content);
                if (request.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
