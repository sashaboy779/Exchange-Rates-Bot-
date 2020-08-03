namespace ExchangeRateApi.Models.TelegramBot.Handlers
{
    public abstract class Handler
    {
        public abstract string MessageText { get; }
    }
}