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
using ExchangeRateApi.Services.Interfaces;

namespace ExchangeRateApi.Controllers
{
    public class MessageController : ApiController
    {
        private readonly ILocalizationService service;
        private readonly IBot bot;
        private readonly TelegramBotClient client;

        public MessageController(ILocalizationService localizationService, IBot exchangeRateBot)
        {
            service = localizationService;
            bot = exchangeRateBot;
            client = exchangeRateBot.Client;
        }

        [Route(AppSettings.WebhookUriPart)] 
        public async Task<IHttpActionResult> Update([FromBody] Update update)
        {
            try
            {
                await service.ApplyUserCultureAsync(GetUserId(update));
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
        
        private int GetUserId(Update update)
        {
            var updateName = Enum.GetName(typeof(UpdateType), update.Type);
            var property = update.GetType().GetProperty(updateName).GetValue(update);
            var user = property.GetType().GetProperty(AppSettings.TelegramBotSenderProperty)
                .GetValue(property) as User;

            return user.Id;
        }
        
        private async Task HadleMessageAsync(Message message, TelegramBotClient client)
        {
            Command command;

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