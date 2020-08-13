using System;
using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Bot.Commands.Hidden;
using ExchangeRateApi.Infrastructure.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class Yesterday : Command
    {
        public override string Identifier => CommandsList.Yesterday;
        private readonly Rate rateCommand;

        public Yesterday(Rate rateCommand)
        {
            this.rateCommand = rateCommand;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            message.Text = DateTime.Now.Date.AddDays(-1).ToString();
            await rateCommand.Execute(message, client);
        }
    }
}