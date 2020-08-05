namespace ExchangeRateApi.Infrastructure.Constants
{
    public static class ServiceSettings
    {
        public static string JsonMediaType => "application/json";
        public static string DateFormat => "dd.MM.yyyy";
        public static string PrivatBankApiUri => "https://api.privatbank.ua/p24api/exchange_rates?json&date={0}";
    }
}