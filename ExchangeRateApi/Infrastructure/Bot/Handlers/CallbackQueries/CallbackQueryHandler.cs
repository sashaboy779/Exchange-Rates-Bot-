using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
    
namespace ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries
{
    public abstract class CallbackQueryHandler : Handler
    {
        public abstract Task Handle(TelegramBotClient client, CallbackQuery callbackQuery);
    }
}