using System.Configuration;

namespace ExchangeRateApi.Infrastructure.Constants
{
    public static class AppSettings
    {
        public static string DatabaseName => "BotDb";
        public static string BotUrl => GetValue("BotUrl");
        public static string BotKey => GetValue("BotKey");
        public static string SupportedCultures => "en,uk";
        public static string DefaultCulture => "en";

        public const string WebhookUriPart = "api/message/update";
        public static string LoggerName => "LOGGER";

        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}