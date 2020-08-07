using System;
using System.Threading.Tasks;
using System.Web.Http;
using ExchangeRateApi.Infrastructure.Bot;
using ExchangeRateApi.Infrastructure.Constants;
using log4net;
using Telegram.Bot;
using Telegram.Bot.Types;

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
                // TODO Add logic
            }
            catch (Exception e)
            {
                LogManager.GetLogger(AppSettings.LoggerName).Error(e.Message, e);
            }
            return Ok();
        }
    }
}