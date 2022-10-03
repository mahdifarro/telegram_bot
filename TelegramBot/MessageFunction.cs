using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class MessageFunction
    {
        public void MessageSender(Telegram.Bot.TelegramBotClient bot, int chatId, string text)
        {
            bot.SendTextMessageAsync(chatId, text,ParseMode.Html);
        }

        public void MessageSender(Telegram.Bot.TelegramBotClient bot, string destinationId, string text)
        {
            bot.SendTextMessageAsync(destinationId, text, ParseMode.Html);
        }
    }
}
