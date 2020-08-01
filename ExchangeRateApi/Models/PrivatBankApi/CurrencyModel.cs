using System.Collections.Generic;

namespace ExchangeRateApi.Models.PrivatBankApi
{
    public class CurrencyModel
    {
        public IDictionary<string, string> CurrencyData { get; set; } = new Dictionary<string, string>();
    }
}