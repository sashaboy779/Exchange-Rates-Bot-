using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class SetCurrency : Command
    {
        public override string Identifier => CommandsList.SetCurrency;

        private readonly IUserService service;

        public SetCurrency(IUserService userService)
        {
            service = userService;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            await client.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var user = await service.FindUserAsync(message.From.Id);

            var inlineKeyboard = InlineKeyboardGenerator.CreateCurrencyKeyboard(user.Currencies);
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: CommandsResources.SetCurrencyAbout,
                replyMarkup: inlineKeyboard
            );
        }
    }
}