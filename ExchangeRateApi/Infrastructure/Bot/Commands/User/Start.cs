using System.Threading.Tasks;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRateApi.Infrastructure.Bot.Commands.User
{
    public class Start : Command
    {
        public override string Identifier => CommandsList.Start;

        private readonly IUserService service;

        public Start(IUserService serviceUser)
        {
            service = serviceUser;
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var welcomeText = string.IsNullOrEmpty(message.Chat.Username) 
                ? CommandsResources.WelcomeWithoutUsername
                : string.Format(CommandsResources.Welcome, message.Chat.Username);

            await client.SendTextMessageAsync(message.Chat.Id, welcomeText);
            await client.SendTextMessageAsync(message.Chat.Id, CommandsResources.Tutorial, ParseMode.Markdown);

            await service.CreateUserAsync(new Models.User.User(message.From.Id, message.From.LanguageCode));
        }
    }
}