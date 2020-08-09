using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries
{
    public class CurrencyKeyboardHandler : CallbackQueryHandler
    {
        public override string MessageText => CommandsResources.SetCurrencyAbout;

        private readonly IUserService service;

        public CurrencyKeyboardHandler(IUserService userService)
        {
            service = userService;
        }

        public override async Task Handle(TelegramBotClient client, CallbackQuery callbackQuery)
        {
            var answer = await UpdateButtonCode(callbackQuery.Message.ReplyMarkup.InlineKeyboard, callbackQuery);

            await client.EditMessageReplyMarkupAsync(
                chatId: callbackQuery.Message.Chat.Id,
                messageId: callbackQuery.Message.MessageId,
                replyMarkup: callbackQuery.Message.ReplyMarkup);

            await client.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: answer);
        }

        private async Task<string> UpdateButtonCode(IEnumerable<IEnumerable<InlineKeyboardButton>> inlineKeyboard,
                                                           CallbackQuery callbackQuery)
        {
            var answer = string.Empty;
            foreach (var row in inlineKeyboard)
            {
                var selectedButton = row.SingleOrDefault(x => x.Text == callbackQuery.Data);
                if (selectedButton != null)
                {
                    var userId = callbackQuery.From.Id;
                    var currencyCode = selectedButton.Text;

                    if (selectedButton.Text.Length == 5)
                    {
                        RemoveCheckMark(selectedButton);
                        currencyCode = selectedButton.Text;
                        answer = string.Format(CommandsResources.CurrencyRemoved, currencyCode);
                    }
                    else
                    {
                        AddCheckMark(selectedButton);
                        answer = string.Format(CommandsResources.CurrencyAdded, currencyCode);
                    }

                    await service.ManageUserCurrencyAsync(userId, (Currencies)Enum.Parse(typeof(Currencies), currencyCode));
                    break;
                }
            }
            return answer;
        }

        private void AddCheckMark(InlineKeyboardButton button)
        {
            button.Text = string.Format(CommandsResources.CheckMark, button.Text);
            button.CallbackData = button.Text;
        }

        private void RemoveCheckMark(InlineKeyboardButton button)
        {
            button.Text = button.Text.Remove(0, 2);
            button.CallbackData = button.Text;
        }
    }
}