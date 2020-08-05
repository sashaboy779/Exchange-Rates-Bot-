using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using ExchangeRateApi.Resources;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        public string GetCurrencies()
        {
            var manager = CurrencyCodes.ResourceManager;
            var text = new StringBuilder();
            var supportedCurrencies = Enum.GetNames(typeof(Currencies));
            text.AppendLine(Messages.AllCurrencies);

            var currencyInfos = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry info in currencyInfos)
            {
                if (supportedCurrencies.Contains(info.Key))
                {
                    text.AppendLine(string.Format(Messages.FormattedCurrencyInfo, info.Key, info.Value));
                }
            }

            return text.ToString();
        }
    }
}