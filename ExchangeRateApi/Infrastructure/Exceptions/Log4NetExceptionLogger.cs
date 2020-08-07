using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace ExchangeRateApi.Infrastructure.Exceptions
{
    public class Log4NetExceptionLogger : ExceptionLogger
    {
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Logger.Log.Fatal(context.Exception.Message, context.Exception);
            return Task.FromResult(0);
        }
    }
}