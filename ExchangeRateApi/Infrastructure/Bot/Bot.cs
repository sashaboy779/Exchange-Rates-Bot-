using System.Collections.Generic;
using System.Linq;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Models.TelegramBot.Commands;
using ExchangeRateApi.Models.TelegramBot.Handlers.CallbackQuery;
using Telegram.Bot;

namespace ExchangeRateApi.Infrastructure.Bot
{
    public class Bot : IBot
    {
        public TelegramBotClient Client { get; }

        private List<Command> userCommands;
        private List<Command> hiddenCommands;
        private List<CallbackQueryHandler> callbackQueryHandlersList;

        public Bot()
        {
            InitializeUserCommands();
            InitializeCallbackQueryHandlers();
            InitializeHiddenCommands();

            Client = new TelegramBotClient(AppSettings.BotKey);
        }

        public Command GetUserCommand(string identifier)
        {
            Command command = null;

            // TODO Add logic

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

        private void InitializeUserCommands()
        {
            userCommands = new List<Command>
            {
                // add commands
            };
        }

        private void InitializeCallbackQueryHandlers()
        {
            callbackQueryHandlersList = new List<CallbackQueryHandler>
            {
                // add callback query handlers
            };
        }

        private void InitializeHiddenCommands()
        {
            hiddenCommands = new List<Command>
            {
                // add hidden commands
            };
        }
    }
}