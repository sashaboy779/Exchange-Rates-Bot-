using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRateApi.Models.PrivatBankApi;
using ExchangeRateApi.Models.TelegramBot;

namespace ExchangeRateApi.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<FilteredExchangeRates> FilterExchangeRatesAsync(int userTelegramId, IEnumerable<ExchangeRate> list);
        Task<IEnumerable<ExchangeRate>> LoadExchangeRateAsync(DateTime date);
    }
}