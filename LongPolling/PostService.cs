using ClientSamokat.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientSamokat.LongPolling
{
    public class PostService
    {
        private HttpClient _httpClient { get; set; } = null!;
        private JsonSerializerOptions? _serializerOptions { get; set; } = null;

        public PostService()
        {
            _serializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,

            };
        }


        public async Task<bool> Post<T>(Action<HttpClient> HttpClientOptions, T objectToSend) where T : ObservableModel
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);

                string jobj = JsonSerializer.Serialize<T>(objectToSend, _serializerOptions);
                StringContent content = new(jobj, Encoding.UTF8, "application/json");

                using HttpResponseMessage request = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
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
