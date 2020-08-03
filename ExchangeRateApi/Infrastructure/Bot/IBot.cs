using ExchangeRateApi.Models.TelegramBot.Commands;
using ExchangeRateApi.Models.TelegramBot.Handlers.CallbackQuery;
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