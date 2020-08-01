using System.Data.Entity;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Migrations;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApi.DataAccess
{
    public class ExchangeRateBotContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserCurrency> UserCurrencies { get; set; }

        public ExchangeRateBotContext() : base(AppSettings.DatabaseName)
        {
        }

        public ExchangeRateBotContext(string connectionStringName) : base(connectionStringName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExchangeRateBotContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Currencies)
                .WithMany(x => x.Users);
        }
    }
}