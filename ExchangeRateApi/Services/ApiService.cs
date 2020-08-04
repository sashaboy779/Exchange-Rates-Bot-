using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient ApiClient;

        public ApiService()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ServiceSettings.JsonMediaType));
        }

        public async Task<HttpResponseMessage> MakeApiCallAsync(string uri)
        {
            return await ApiClient.GetAsync(uri);
        }
    }
}