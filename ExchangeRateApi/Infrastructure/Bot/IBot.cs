using ExchangeRateApi.Infrastructure.Bot.Commands;
using ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries;
using Telegram.Bot;

namespace ExchangeRateApi.Infrastructure.Bot
{
    public interface IBot
    {
        TelegramBotClient Client { get; }

        CallbackQueryHandler GetCallbackQueryHandler(string messageText);
        Command GetUserCommand(string identifier);
        Command GetHiddenCommand(string identifier);
    }
}