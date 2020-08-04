using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface IApiService
    {
        Task<HttpResponseMessage> MakeApiCallAsync(string uri);
    }
}