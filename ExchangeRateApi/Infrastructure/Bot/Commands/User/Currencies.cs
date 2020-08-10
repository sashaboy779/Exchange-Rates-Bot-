using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class Currencies : Command
    {
        public override string Identifier => CommandsList.Currencies;

        private readonly ICurrenciesService service;

        public Currencies(ICurrenciesService currenciesService)
        {
            service = currenciesService;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            var currenciesInfo = service.GetCurrencies();
            await client.SendTextMessageAsync(chatId, currenciesInfo, ParseMode.Markdown);
        }
    }
}