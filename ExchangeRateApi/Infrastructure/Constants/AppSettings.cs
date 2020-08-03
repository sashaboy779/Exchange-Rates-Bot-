using System.Configuration;

namespace ExchangeRateApi.Infrastructure.Constants
{
    public static class AppSettings
    {
        public static string DatabaseName => "BotDb";
        public static string BotUrl => GetValue("BotUrl");
        public static string BotKey => GetValue("BotKey");
        public const string WebhookUriPart = "api/message/update";

        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}