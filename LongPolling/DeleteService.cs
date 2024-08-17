using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientSamokat.LongPolling
{
    public class DeleteService
    {
        private HttpClient _httpClient { get; set; } = null!;
        private JsonSerializerOptions? _serializerOptions { get; set; } = null;

        public DeleteService()
        {
            _serializerOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,

            };
        }

        public async Task<bool> Delete(Action<HttpClient> HttpClientOptions)
        {
            using (_httpClient = new HttpClient())
            {
                HttpClientOptions.Invoke(_httpClient);
                _httpClient.Timeout = new TimeSpan(0, 1, 0);
                HttpRequestMessage request = new(HttpMethod.Delete, _httpClient.BaseAddress);
                using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

    }
}
