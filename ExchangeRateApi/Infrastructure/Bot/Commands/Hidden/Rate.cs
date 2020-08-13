using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Infrastructure.Exceptions;
using ExchangeRateApi.Models.PrivatBankApi;
using ExchangeRateApi.Models.TelegramBot;
using ExchangeRateApi.Models.User;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.Hidden
{
    public class Rate : Command
    {
        public override string Identifier => CommandsList.Rate;

        private readonly IExchangeRateService service;

        public Rate(IExchangeRateService exchangeRateService)
        {
            service = exchangeRateService;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var text = CommandsResources.NoExchangeRateFound;

            await client.SendChatActionAsync(chatId, ChatAction.Typing);

            try
            {
                var exchangeRate = service.LoadExchangeRateAsync(DateTime.Parse(message.Text)).Result.ToList();

                if (exchangeRate.Any())
                {
                    var filteredExchangeRate = await service.FilterExchangeRatesAsync(message.From.Id, exchangeRate);
                    text = ConvertExchangeRates(filteredExchangeRate);
                }

                await client.SendTextMessageAsync(chatId, text, ParseMode.Markdown);
            }
            catch (ExchangeRateNotFoundException)
            {
                await client.SendTextMessageAsync(chatId, text);
            }
        }

        private string ConvertExchangeRates(FilteredExchangeRates exchangeRates)
        {
            if (exchangeRates.Filtered.Any() || exchangeRates.NotFound.Any())
            {
                var text = new StringBuilder();

                if (exchangeRates.Filtered.Any())
                {
                    ConvertFilteredExchangeRates(text, exchangeRates.Filtered);
                }

                if (exchangeRates.NotFound.Any())
                {
                    ConvertNotFoundExchangeRates(text, exchangeRates.NotFound);
                }

                return text.ToString();
            }

            return CommandsResources.NoSelectedRates;
        }

        private void ConvertFilteredExchangeRates(StringBuilder text, IEnumerable<ExchangeRate> list)
        {
            text.AppendLine(CommandsResources.AboutBank);

            foreach (var item in list)
            {
                text.AppendLine(string.Format(CommandsResources.CurrencyExchangeCode, item.Currency));
                if (item.SaleRate != 0)
                {
                    text.AppendLine(string.Format(CommandsResources.SaleRateNBU, item.SaleRate));
                    text.AppendLine(string.Format(CommandsResources.PurchaseRateNBU, item.PurchaseRate));
                }

                if (item.SaleRateNb != 0)
                {
                    text.AppendLine(string.Format(CommandsResources.SaleRatePB, item.SaleRateNb));
                    text.AppendLine(string.Format(CommandsResources.PurchaseRatePB, item.PurchaseRateNb));
                }
                text.AppendLine();
            }
        }
        private void ConvertNotFoundExchangeRates(StringBuilder text, IEnumerable<UserCurrency> list)
        {
            text.AppendLine();
            text.AppendLine(CommandsResources.NotFoundExchangeRates);

            foreach (var currency in list)
            {
                text.AppendLine(Enum.GetName(typeof(Currencies), currency.Currency));
            }
        }
    }
}