using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface IApiService
    {
        HttpResponseMessage MakeApiCall(string uri);
    }
}