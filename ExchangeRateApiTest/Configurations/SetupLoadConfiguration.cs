using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using ExchangeRateApi.Models.PrivatBankApi;
using ExchangeRateApi.Services.Interfaces;
using Moq;

namespace ExchangeRateApiTest.Configurations
{
    public class SetupLoadConfiguration
    {
        public Mock<ObjectCache> MockCache { get; set; }
        public Mock<IApiService> MockApi { get; set; }
        public HttpResponseMessage MessageReturnedByApi { get; set; } = null;
        public IEnumerable<ExchangeRate> ExchangeRateReturnedByCache { get; set; } = null;
    }
}