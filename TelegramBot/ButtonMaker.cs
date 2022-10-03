using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class ButtonMaker
    {
        public KeyboardButton[] ReplyRowKeyboardBuilder(params KeyboardButton[] buttons)
        {
            KeyboardButton[] replyKeyboardRow = new KeyboardButton[buttons.Length];
            int i = 0;
            foreach (KeyboardButton button in buttons)
            {
                replyKeyboardRow[i] = button;
                i++;
            }

            return replyKeyboardRow;
        }

        public InlineKeyboardButton[] InlineRowKeyboardBuilder(params InlineKeyboardButton[] buttons)
        {
            InlineKeyboardButton[] inlineKeyboardRow = new InlineKeyboardButton[buttons.Length];
            int i = 0;
            foreach(InlineKeyboardButton button in buttons)
            {
                inlineKeyboardRow[i] = button;
                i++;
            }
            return inlineKeyboardRow;

        }
    }
}
