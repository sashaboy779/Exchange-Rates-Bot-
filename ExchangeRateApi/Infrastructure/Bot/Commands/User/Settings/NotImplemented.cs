using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User.Settings
{
    public class NotImplemented : Command
    {
        public override string Identifier => CommandsResources.SettingsSoon;

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, CommandsResources.NotImplemented);
        }
    }
}