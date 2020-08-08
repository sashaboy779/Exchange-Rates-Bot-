namespace ExchangeRateApi.Infrastructure.Bot.Handlers
{
    public abstract class Handler
    {
        public abstract string MessageText { get; }
    }
}