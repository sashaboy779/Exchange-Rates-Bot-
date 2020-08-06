using System;
using System.Collections.Generic;
using ExchangeRateApi.Models.PrivatBankApi;

namespace ExchangeRateApiTest.Fixtures
{
    public class ExchangeRateFixture
    {
        public DateTime Date { get; }
        public string DateString { get; }
        public ExchangeRateModel ExchangeRateModel { get; }
        public IEnumerable<ExchangeRate> NotFilteredExchangeRates { get; }

        public ExchangeRateFixture()
        {
            Date = new DateTime(2018, 08, 06);
            DateString = "06.08.2018";

            ExchangeRateModel = new ExchangeRateModel
            {
                ExchangeRate = new List<ExchangeRate>()
            };

            NotFilteredExchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate { Currency = "CAD" },
                new ExchangeRate { Currency = "XAU" },
                new ExchangeRate { Currency = "EUR" },
                new ExchangeRate { Currency = "SEK" }
            };
        }
    }
}