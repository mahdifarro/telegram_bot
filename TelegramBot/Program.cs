using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelegramBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BotMain());
        }
    }
}


//if (up.CallbackQuery != null)
//                    {
//                        switch (up.CallbackQuery.Data)
//                        {
//                            case "1":
//                                {
//                                    okCount += 1;
//                                    break;
//                                }
//                            case "2":
//                                {
//                                    notOkCount += 1;
//                                    break;
//                                }

//                        }

//                        StringBuilder sb = new StringBuilder();
//sb.AppendLine("ok san?");

//                        InlineKeyboardButton[][] inline =
//                        {
//                            new []
//                            {
//                                new InlineKeyboardButton()
//                                {
//                                Text="ok",Url="https://google.com/"
//                                },
//                            new InlineKeyboardButton()
//                                {
//                                Text="google",Url="https://github.com/TelegramBots/telegram.bot/issues/349"
//                                }
//                            },

//                            new []
//                            {
//                                new InlineKeyboardButton()
//                                {
//                                Text="yup "+okCount,CallbackData="1"
//                                },
//                                new InlineKeyboardButton()
//                                {
//                                Text="nop "+notOkCount,CallbackData="2"
//                                }
//                            }
//                        };

//InlineKeyboardMarkup inlineContactKeyboardMarkup = new InlineKeyboardMarkup(inline);
//bot.EditMessageReplyMarkupAsync(up.CallbackQuery.Message.Chat.Id, up.CallbackQuery.Message.MessageId, inline);


