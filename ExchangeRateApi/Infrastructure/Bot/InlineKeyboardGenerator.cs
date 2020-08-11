using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Resources;
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
        
        public static InlineKeyboardMarkup CreateLanguagesKeyboard()
        {
            var manager = new ResourceManager(typeof(Languages));
            var languages = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            
            var rows = new List<IEnumerable<InlineKeyboardButton>>();
            var currentRow = new List<InlineKeyboardButton>();
            
            rows.Add(currentRow);
            var currentButton = 0;
            
            foreach (DictionaryEntry language in languages)
            {
                var languageName = language.Key.ToString();
                currentRow.Add(InlineKeyboardButton.WithCallbackData(languageName));
                currentButton++;

                currentRow = TryAddNextRow(currentButton, 2, rows, currentRow);
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