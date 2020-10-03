using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WowHelp.Shared
{
    public static class HttpClientExtensions
    {
        public static async Task<T> Send<T>(this HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            return await response.ConvertResponse<T>();
        }

        public static async Task<T> Get<T>(this HttpClient client, string url)
        {
            var response = await client.GetAsync(url);

            return await response.ConvertResponse<T>();
        }

        public static async Task<T> ConvertResponse<T>(this HttpResponseMessage response)
        {
            if (response is null || !response.IsSuccessStatusCode || response.Content is null)
                return default;

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return default;

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}