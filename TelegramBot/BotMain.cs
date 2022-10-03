using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public partial class BotMain : Form
    {

        ButtonMaker buttonMaker = new ButtonMaker();

        private static string token = "";
        private Thread botThread;
        private Telegram.Bot.TelegramBotClient bot;
        private ReplyKeyboardMarkup mainKeyboardMarkup;

        public BotMain()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            token = textToken.Text;
            botThread = new Thread(new ThreadStart(RunBot));
            botThread.Start();
        }

        private void BotMain_Load(object sender, EventArgs e)
        {
            KeyboardButton[] mainRow1 = buttonMaker.ReplyRowKeyboardBuilder(new KeyboardButton("contact us " + "\U0000270F"), new KeyboardButton("About Us"));
            KeyboardButton[] mainRow2 = buttonMaker.ReplyRowKeyboardBuilder(new KeyboardButton("Vote" + "\U00002B06"));
            KeyboardButton[][] replyMainKeyboard = new KeyboardButton[][]
            {
                mainRow1,mainRow2
            };
            mainKeyboardMarkup = new ReplyKeyboardMarkup(replyMainKeyboard, true);

        }

        void RunBot()
        {
            bot = new Telegram.Bot.TelegramBotClient(token);
            this.Invoke(new Action(() =>
            {
                labelStatus.Text = "Online";
                labelStatus.ForeColor = Color.Green;
                buttonStart.Text = "Stop";
            }
            ));

            int offset = 0;

            while (true)
            {
                Telegram.Bot.Types.Update[] update = bot.GetUpdatesAsync(offset).Result;
                foreach (var up in update)
                {
                    if (up.Message != null)
                    {
                        offset = up.Id + 1;

                        string text = up.Message.Text.ToLower();
                        var from = up.Message.From;
                        var chatId = up.Message.Chat.Id;

                        if (text.Contains("/start") || text.Contains("/main") || text.Contains("main"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(from.Username + "عزیز،خوش آمدید!");
                            sb.AppendLine("اسم سهام؟");
                            //sb.AppendLine("برای شناختن امکانات ما: /AboutUs");
                            //sb.AppendLine("برای ارتباط با ما: /ContactUs");

                            bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, false, false, 0, mainKeyboardMarkup);
                        }
                        else if (text.Contains("/aboutus") || text.Contains("about us"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("I'm bot :)");
                            KeyboardButton[] aboutRow1 = buttonMaker.ReplyRowKeyboardBuilder(new KeyboardButton("Back"));
                            ReplyKeyboardMarkup replyAboutKeyboardMarkup = new ReplyKeyboardMarkup(aboutRow1, true);

                            bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, false, false, 0, replyAboutKeyboardMarkup);
                        }
                        else if (text.Contains("/contactus") || text.Contains("contact us"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("0914#######");

                            KeyboardButton[] contactRow1 = buttonMaker.ReplyRowKeyboardBuilder(new KeyboardButton("contact me"), new KeyboardButton("contact team"));
                            KeyboardButton[] contactRow2 = buttonMaker.ReplyRowKeyboardBuilder(new KeyboardButton("back"));
                            KeyboardButton[][] replyContactKeyboard = { contactRow1, contactRow2 };
                            ReplyKeyboardMarkup replyContactKeyboardMarkup = new ReplyKeyboardMarkup(replyContactKeyboard,true);

                            bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, false, false, 0, replyContactKeyboardMarkup);

                            InlineKeyboardButton[] inlineContactRow1 = buttonMaker.InlineRowKeyboardBuilder(InlineKeyboardButton.WithUrl("admin man", "https://twitter.com"),InlineKeyboardButton.WithCallbackData("ok"));
                            InlineKeyboardButton[][] inlineContactKeyboard = { inlineContactRow1 };
                            InlineKeyboardMarkup inlineContactKeyboardMarkup = new InlineKeyboardMarkup(inlineContactRow1);

                            bot.SendTextMessageAsync(chatId,sb.ToString(), ParseMode.Default, false, false, 0, inlineContactKeyboardMarkup);
                        }
                        else if (text.Contains("/vote") || text.Contains("vote"))
                        {
                            string[] question = { "good", "bad" };

                            bot.SendPollAsync(chatId, "نظرتون راجب من?", question);
                        }
                        else if (text.Contains("/back") || text.Contains("back"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("going back...");
                            bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Default, false, false, 0, mainKeyboardMarkup);
                        }
                        else if(text.Contains("_"))
                        {
                            text = text.Split('_')[1];   
                            FileFunction fileUploader = new FileFunction();
                            fileUploader.CreateScreenshot(text);

                            string filePath = "E:\\" + text + ".jpg";
                            string caption = textMessage.Text;
                            Stream stream = File.OpenRead(filePath);
                            InputOnlineFile file = new InputOnlineFile(stream);
                            bot.SendPhotoAsync(chatId, file);

                            textMessage.Text = "";
                            textFilePath.Text = "";
                        }

                        dataGridViewReport.Invoke(new Action(() =>
                        {
                            dataGridViewReport.Rows.Add(chatId, from.Username, text, up.Message.MessageId, up.Message.Date.ToString("yyyy/MM/dd - HH/mm"));
                        }));
                    }
                }
            }

        }

        private void BotMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            botThread.Abort();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            MessageFunction messageFunction = new MessageFunction();
            if (dataGridViewReport.CurrentRow != null && textDestinationId.Text=="")
            {
                int chatId = int.Parse(dataGridViewReport.CurrentRow.Cells[0].Value.ToString());
                string text = textMessage.Text;

                bot.SendTextMessageAsync(chatId, text, ParseMode.Html, true);

                textMessage.Text = "";

            }
            else if (textDestinationId.Text != "")
            {
                string destinationId = textDestinationId.Text;
                string text = textMessage.Text;

                bot.SendTextMessageAsync(destinationId, text, ParseMode.Html);
                textMessage.Text = "";
                textDestinationId.Text = "";
            }
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textFilePath.Text = openFile.FileName;
            }
        }

        private void uploadFile_Click(object sender, EventArgs e)
        {
            FileFunction fileUploader = new FileFunction();
            if (dataGridViewReport.CurrentRow != null && textDestinationId.Text == "")
            {
                int chatId = int.Parse(dataGridViewReport.CurrentRow.Cells[0].Value.ToString());
                string filePath = textFilePath.Text;
                string caption = textMessage.Text;

                fileUploader.UploadFile(bot, chatId, filePath, caption);

                textMessage.Text = "";
                textFilePath.Text = "";

            }
            else if (textDestinationId.Text != "")
            {
                string destinationId = textDestinationId.Text;
                string filePath = textFilePath.Text;
                string caption = textMessage.Text;

                fileUploader.UploadFile(bot, destinationId, filePath, caption);

                textMessage.Text = "";
                textFilePath.Text = "";
                textDestinationId.Text = "";
            }
        }

        private void textDestinationId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
