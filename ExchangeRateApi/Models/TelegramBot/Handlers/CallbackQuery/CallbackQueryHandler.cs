﻿using System.Threading.Tasks;
using Telegram.Bot;

namespace ExchangeRateApi.Models.TelegramBot.Handlers.CallbackQuery
{
    public abstract class CallbackQueryHandler : Handler
    {
        public abstract Task Handle(TelegramBotClient client, Telegram.Bot.Types.CallbackQuery callbackQuery);
    }
}