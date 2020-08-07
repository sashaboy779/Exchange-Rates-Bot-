using ExchangeRateApi.Infrastructure.Constants;
using log4net;

namespace ExchangeRateApi.Infrastructure
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger(AppSettings.LoggerName);
    }
}