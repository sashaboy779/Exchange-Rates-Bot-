using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries
{
    public class OldKeyboardHandler : CallbackQueryHandler
    {
        public override string MessageText => CommandsList.OldKeyboard;

        public override async Task Handle(TelegramBotClient client, CallbackQuery callbackQuery)
        {
            try
            {
                var chatId = callbackQuery.Message.Chat.Id;
                await client.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId);
                await client.SendTextMessageAsync(chatId, CommandsResources.OldKeyboard);
            }
            catch (ApiRequestException)
            {
                // keyboard is already deleted
            }
        }
    }
}