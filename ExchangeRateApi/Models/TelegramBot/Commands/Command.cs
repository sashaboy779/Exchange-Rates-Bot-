using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExchangeRateApi.Models.TelegramBot.Commands
{
    public abstract class Command
    {
        public abstract string Identifier { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);
    }
}