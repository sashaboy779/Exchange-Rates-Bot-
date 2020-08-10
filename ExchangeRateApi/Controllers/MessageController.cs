using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot.Types;
using System;
using Telegram.Bot.Types.Enums;
using log4net;
using Telegram.Bot;
using ExchangeRateApi.Infrastructure.Bot;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Infrastructure.Bot.Commands;

namespace ExchangeRateApi.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IBot bot;
        private readonly TelegramBotClient client;

        public MessageController(IBot bot)
        {
            this.bot = bot;
            client = bot.Client;
        }

        [Route(AppSettings.WebhookUriPart)] 
        public async Task<IHttpActionResult> Update([FromBody] Update update)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await HadleMessageAsync(update.Message, client);
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQueryAsync(update, client);
                        break;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger(AppSettings.LoggerName).Error(e.Message, e);
            }
            return Ok();
        }
        
        private async Task HadleMessageAsync(Message message, TelegramBotClient client)
        {
            Command command = null;

            if (message.Text == null)
            {
                command = bot.GetUserCommand(CommandsList.Tutorial);
            }
            else if (DateTime.TryParse(message.Text, out _))
            {
                command = bot.GetHiddenCommand(CommandsList.Rate);
            }
            else
            {
                command = bot.GetUserCommand(message.Text);
            }

            await command.Execute(message, client);
        }
        
        private async Task HandleCallbackQueryAsync(Update update, TelegramBotClient client)
        {
            var handler = bot.GetCallbackQueryHandler(update.CallbackQuery.Message.Text);
            await handler.Handle(client, update.CallbackQuery);
            // TODO Add old keyboard handler
        }
    }
}