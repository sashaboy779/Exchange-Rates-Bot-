using System;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Bot.Commands.Hidden;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class Today : Command
    {
        public override string Identifier => CommandsList.Today;
        private readonly Rate rateCommand;

        public Today(Rate rateCommand)
        {
            this.rateCommand = rateCommand;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            message.Text = DateTime.Now.Date.ToString();
            await rateCommand.Execute(message, client);
        }
    }
}