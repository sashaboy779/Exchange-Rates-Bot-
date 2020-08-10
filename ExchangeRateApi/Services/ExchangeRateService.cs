using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Infrastructure.Exceptions;
using ExchangeRateApi.Models.PrivatBankApi;
using ExchangeRateApi.Models.TelegramBot;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IUserService service;
        private readonly IApiService apiService;
        private readonly ObjectCache cache;

        public ExchangeRateService(IUserService userService, IApiService apiService, ObjectCache defaultMemoryCache)
        {
            service = userService;
            this.apiService = apiService;
            cache = defaultMemoryCache;
        }

        public async Task<IEnumerable<ExchangeRate>> LoadExchangeRateAsync(DateTime date)
        {
            var dateString = date.ToString(ServiceSettings.DateFormat);
            var exchangeRate = cache[dateString] as IEnumerable<ExchangeRate>;

            if (exchangeRate == null)
            {
                var uri = string.Format(ServiceSettings.PrivatBankApiUri, dateString);

                using var response = apiService.MakeApiCall(uri);
                if (response.IsSuccessStatusCode)
                {
                    var model = await response.Content.ReadAsAsync<ExchangeRateModel>();
                    exchangeRate = model.ExchangeRate;

                    cache.Set(dateString, model.ExchangeRate, new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15.0)
                    });
                }
                else
                {
                    throw new ExchangeRateNotFoundException();
                }
            }

            return exchangeRate;
        }

        public async Task<FilteredExchangeRates> FilterExchangeRatesAsync(int userTelegramId, IEnumerable<ExchangeRate> list)
        {
            var user = await service.FindUserAsync(userTelegramId);
            var selectedCurrencies = user.Currencies;

            var filtered = list.Where(x => selectedCurrencies.Any(c =>
                                      Enum.GetName(typeof(Currencies), c.Currency) == x.Currency));

            var notFound = selectedCurrencies.Where(x => filtered.All(c =>
                                                    c.Currency != Enum.GetName(typeof(Currencies), x.Currency)));

            return new FilteredExchangeRates(filtered, notFound);
        }
    }
}