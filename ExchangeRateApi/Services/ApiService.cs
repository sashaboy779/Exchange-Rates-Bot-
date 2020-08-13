using System.Net.Http;
using System.Net.Http.Headers;
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

        public HttpResponseMessage MakeApiCall(string uri)
        {
            var response = ApiClient.GetAsync(uri);
            response.Wait();

            return response.Result;
        }
    }
}