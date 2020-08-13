using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.Hidden
{
    public class ErrorCommand : Command
    {
        public override string Identifier => CommandsList.Error;

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendChatActionAsync(chatId, ChatAction.Typing);
            await client.SendTextMessageAsync(chatId, CommandsResources.NoCommand);
        }
    }
}