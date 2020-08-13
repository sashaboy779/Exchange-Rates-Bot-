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
        }
    }
}
