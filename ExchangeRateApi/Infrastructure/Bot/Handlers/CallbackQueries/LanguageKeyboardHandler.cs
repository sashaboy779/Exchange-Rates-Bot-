using System.Threading.Tasks;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries
{
    public class LanguageKeyboardHandler : CallbackQueryHandler
    {
        public override string MessageText => CommandsResources.SelectLanguage;

        private readonly ILocalizationService service;

        public LanguageKeyboardHandler(ILocalizationService localizationService)
        {
            service = localizationService;
        }

        public override async Task Handle(TelegramBotClient client, CallbackQuery callbackQuery)
        {
            var chatId = callbackQuery.Message.Chat.Id;
            var userTelegramId = callbackQuery.From.Id;

            try
            {
                await client.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId);
                await client.SendChatActionAsync(chatId, ChatAction.Typing);
                var successfully = await service.AddUserLanguageAsync(userTelegramId, callbackQuery.Data);

                if (successfully)
                {
                    await service.ApplyUserCultureAsync(userTelegramId);
                    await client.SendTextMessageAsync(chatId, CommandsResources.LanguageChanged,
                        replyMarkup: new ReplyKeyboardRemove());
                }
            }
            catch (ApiRequestException)
            {
                // language is already changed
            }
        }
    }
}