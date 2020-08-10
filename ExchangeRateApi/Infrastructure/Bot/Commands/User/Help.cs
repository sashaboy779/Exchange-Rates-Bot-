using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class Help : Command
    {
        public override string Identifier => CommandsList.Help;

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendChatActionAsync(chatId, ChatAction.Typing);
            await client.SendTextMessageAsync(chatId, CommandsResources.CommandsList, ParseMode.Markdown);
        }
    }
}