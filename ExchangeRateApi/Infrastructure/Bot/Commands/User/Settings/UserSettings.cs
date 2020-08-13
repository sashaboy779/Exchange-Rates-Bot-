using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User.Settings
{
    public class UserSettings : Command
    {
        public override string Identifier => CommandsList.UserSettings;

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            var keyboard = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { CommandsResources.SettingsLanguage },
                    new KeyboardButton[] { CommandsResources.SettingsSoon },
                    new KeyboardButton[] { CommandsResources.SettingsSoon }
                },
                true, true
            );

            await client.SendTextMessageAsync(chatId, CommandsResources.MainSettings, replyMarkup: keyboard);
        }
    }
}