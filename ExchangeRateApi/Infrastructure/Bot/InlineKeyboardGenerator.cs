using System;
using System.Collections.Generic;
using System.Linq;
using ExchangeRateApi.Models.User;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRateApi.Infrastructure.Bot
{
    public static class InlineKeyboardGenerator
    {
        public static InlineKeyboardMarkup CreateCurrencyKeyboard(IEnumerable<UserCurrency> pickedCurrencies)
        {
            var rows = new List<IEnumerable<InlineKeyboardButton>>();
            var currentRow = new List<InlineKeyboardButton>();
            var allCurrencies = Enum.GetValues(typeof(Currencies));
            
            rows.Add(currentRow);
            var currentButton = 0;
            
            foreach (Currencies currency in allCurrencies)
            {
                var buttonText = Enum.GetName(typeof(Currencies), currency);

                if (IsCurrencyPicked(currency, pickedCurrencies))
                {
                    buttonText = string.Format(CommandsResources.CheckMark, buttonText); 
                }

                currentRow.Add(InlineKeyboardButton.WithCallbackData(buttonText));
                currentButton++;
                currentRow = TryAddNextRow(currentButton, 3, rows, currentRow);
            }

            return new InlineKeyboardMarkup(rows);
        }

        private static List<InlineKeyboardButton> TryAddNextRow(int buttonNumber, int totalButtonInRow, 
                            List<IEnumerable<InlineKeyboardButton>> rows, List<InlineKeyboardButton> currentRow)
        {
            if (buttonNumber % totalButtonInRow == 0)
            {
                currentRow = new List<InlineKeyboardButton>();
                rows.Add(currentRow);
            }

            return currentRow;
        }

        private static bool IsCurrencyPicked(Currencies current, IEnumerable<UserCurrency> picked)
        {
            return picked.SingleOrDefault(x => x.Currency.ToString() == current.ToString()) != null;
        }
    }
}