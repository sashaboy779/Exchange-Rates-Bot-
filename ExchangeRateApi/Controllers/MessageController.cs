using System;
using System.Threading.Tasks;
using System.Web.Http;
using ExchangeRateApi.Infrastructure.Bot;
using ExchangeRateApi.Infrastructure.Bot.Commands;
using ExchangeRateApi.Infrastructure.Constants;
using log4net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
                    default:
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
                // TODO Add tutorial command
            }
            else if (DateTime.TryParse(message.Text, out _))
            {
                // TODO Add exchange rate by date command
            }
            else
            {
                command = bot.GetUserCommand(message.Text);
            }

            await command.Execute(message, client);
        }
    }
}