using System.Collections.Generic;
using ExchangeRateApi.Models.PrivatBankApi;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.Models.TelegramBot
{
    public class FilteredExchangeRates
    {
        public IEnumerable<ExchangeRate> Filtered { get; set; }
        public IEnumerable<UserCurrency> NotFound { get; set; }

        public FilteredExchangeRates(IEnumerable<ExchangeRate> filtered, IEnumerable<UserCurrency> notFound)
        {
            Filtered = filtered;
            NotFound = notFound;
        }
    }
}