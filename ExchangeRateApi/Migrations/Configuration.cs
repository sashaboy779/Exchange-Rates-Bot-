using ExchangeRateApi.Models.User;
using System.Data.Entity.Migrations;

namespace ExchangeRateApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.ExchangeRateBotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.ExchangeRateBotContext context)
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

            context.UserCurrencies.AddOrUpdate(currencies);
            context.SaveChanges();
        }
    }
}
