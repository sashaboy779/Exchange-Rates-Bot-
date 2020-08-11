using System.Collections.Generic;
using System.Linq;
using ExchangeRateApi.Infrastructure.Bot.Commands;
using ExchangeRateApi.Infrastructure.Bot.Commands.Hidden;
using ExchangeRateApi.Infrastructure.Bot.Commands.User;
using ExchangeRateApi.Infrastructure.Bot.Handlers.CallbackQueries;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;

namespace ExchangeRateApi.Infrastructure.Bot
{
    public class Bot : IBot
    {
        public TelegramBotClient Client { get; }

        private List<Command> userCommands;
        private List<Command> hiddenCommands;
        private List<CallbackQueryHandler> callbackQueryHandlersList;

        public Bot(IUserService userService, IExchangeRateService exchangeRateService, 
            ICurrenciesService currenciesService)
        {
            InitializeUserCommands(userService, currenciesService);
            InitializeCallbackQueryHandlers(userService);
            InitializeHiddenCommands(exchangeRateService);

            Client = new TelegramBotClient(AppSettings.BotKey);
        }

        public Command GetUserCommand(string identifier)
        {
            Command command = null;

            if (identifier != "/")
            {
                command = userCommands.SingleOrDefault(x => x.Identifier == identifier);
            }
            if (command == null)
            {
                if (identifier.StartsWith("/"))
                {
                    command = hiddenCommands.Single(x => x.Identifier == CommandsList.Error);
                }
                else
                {
                    command = hiddenCommands.Single(x => x.Identifier == CommandsList.IncorrectDateFormat);
                }
            }

            return command;
        }

        public Command GetHiddenCommand(string identifier)
        {
            return hiddenCommands.Single(x => x.Identifier == identifier);
        }

        public CallbackQueryHandler GetCallbackQueryHandler(string messageText)
        {
            return callbackQueryHandlersList.SingleOrDefault(x => x.MessageText == messageText);
        }

        public static void SetWebhook()
        {
            var client = new TelegramBotClient(AppSettings.BotKey);
            var webhook = string.Format(AppSettings.BotUrl, AppSettings.WebhookUriPart);
            _ = client.SetWebhookAsync(webhook, maxConnections: 40);
        }

        private void InitializeUserCommands(IUserService userService, ICurrenciesService currenciesService)
        {
            userCommands = new List<Command>
            {
                new Start(userService),
                new Tutorial(),
                new SetCurrency(userService),
                new Help(),
                new Currencies(currenciesService)
            };
        }

        private void InitializeCallbackQueryHandlers(IUserService userService)
        {
            callbackQueryHandlersList = new List<CallbackQueryHandler>
            {
                new CurrencyKeyboardHandler(userService)
            };
        }

        private void InitializeHiddenCommands(IExchangeRateService exchangeRateService)
        {
            hiddenCommands = new List<Command>
            {
                new Rate(exchangeRateService),
                new ErrorCommand(),
                new IncorrectDateFormat()
            };
        }
    }
}