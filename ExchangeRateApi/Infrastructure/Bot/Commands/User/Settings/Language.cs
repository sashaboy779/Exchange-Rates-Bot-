using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User.Settings
{
    public class Language : Command
    {
        public override string Identifier => CommandsResources.SettingsLanguage;

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            var inlineKeyboard = InlineKeyboardGenerator.CreateLanguagesKeyboard();
            await client.SendTextMessageAsync(chatId, CommandsResources.SelectLanguage, replyMarkup: inlineKeyboard);
        }
    }
}