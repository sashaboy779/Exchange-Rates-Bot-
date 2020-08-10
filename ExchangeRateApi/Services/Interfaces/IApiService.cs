using System.Net.Http;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface IApiService
    {
        HttpResponseMessage MakeApiCall(string uri);
    }
}