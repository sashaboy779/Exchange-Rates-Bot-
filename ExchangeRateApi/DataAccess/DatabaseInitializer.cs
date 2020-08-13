using ExchangeRateApi.Models.User;
using System.Data.Entity;

namespace ExchangeRateApi.DataAccess
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<ExchangeRateBotContext>
    {
        protected override void Seed(ExchangeRateBotContext context)
        {
            var currencies = new[]
            {
                new UserCurrency { Currency = Currencies.USD },
                new UserCurrency { Currency = Currencies.EUR },
                new UserCurrency { Currency = Currencies.RUB },
                new UserCurrency { Currency = Currencies.CHF },
                new UserCurrency { Currency = Currencies.GBP },
                new UserCurrency { Currency = Currencies.SEK },
                new UserCurrency { Currency = Currencies.XAU },
                new UserCurrency { Currency = Currencies.CAD }
            };

            context.UserCurrencies.AddRange(currencies);
            context.SaveChanges();
        }
    }
}