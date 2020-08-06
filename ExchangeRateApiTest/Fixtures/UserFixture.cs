using System.Collections.Generic;
using ExchangeRateApi.Models.User;

namespace ExchangeRateApiTest.Fixtures
{
    public class UserFixture 
    {
        public User EmptyUser { get; }
        public User UserWithCurrencies { get; }
        public User UserWithLanguageCode { get; }
        public UserCurrency RubCurrency { get; }
        public int UserId { get; }

        public UserFixture()
        {
            UserId = 123456;
            
            EmptyUser = new User
            {
                UserTelegramId = 123456
            };
            
            UserWithCurrencies = new User
            {
                Currencies = new List<UserCurrency>
                {
                    new UserCurrency {Currency = Currencies.EUR},
                    new UserCurrency {Currency = Currencies.USD}
                },
                UserTelegramId = 123456
            };

            UserWithLanguageCode = new User
            {
                LanguageCode = "uk"
            };

            RubCurrency = new UserCurrency
            {
                Currency = Currencies.RUB
            };
        }
    }
}