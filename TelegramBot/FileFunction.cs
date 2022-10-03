using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace TelegramBot
{
    class FileFunction
    {
        private string FileFormat(string filePath)
        {
            string[] parts = filePath.Split('.');
            string fileFormat = parts[parts.Length - 1].ToLower();
            if (fileFormat.Equals("png") || fileFormat.Equals("jpg") || fileFormat.Equals("bmp"))
            {
                fileFormat = "picture";
            }
            else if (fileFormat.Equals("mp4") || fileFormat.Equals("avi") || fileFormat.Equals("wmv") || fileFormat.Equals("flv"))
            {
                fileFormat = "video";
            }
            else if (fileFormat.Equals("mp3"))
            {
                fileFormat = "audio";
            }
            else
            {
                fileFormat = "file";
            }
            return fileFormat;
        }

        public void CreateScreenshot(string sahmName) {
            WebsitesScreenshot.WebsitesScreenshot _Obj;
            _Obj = new WebsitesScreenshot.WebsitesScreenshot();
            WebsitesScreenshot.WebsitesScreenshot.Result _Result;
            _Result = _Obj.CaptureWebpage("https://www."+sahmName+".com/");
            if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
            {
                _Obj.ImageFormat = WebsitesScreenshot.
                    WebsitesScreenshot.ImageFormats.JPG;
                _Obj.SaveImage("E:\\"+sahmName+".jpg");
            }
            _Obj.Dispose();
        }

        public void UploadFile(Telegram.Bot.TelegramBotClient bot, int chatId, string filePath, string caption = null)
        {
            if(filePath == string.Empty)
            {
                return;
            }
            string fileFormat = FileFormat(filePath);
            if (fileFormat.Equals("picture"))
            {

                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendPhotoAsync(chatId, file, caption);
            }
            else if (fileFormat.Equals("video"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendVideoAsync(chatId, file, default, default, default, caption);
            }
            else if (fileFormat.Equals("file"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendDocumentAsync(chatId, file, caption);
            }
            else if (fileFormat.Equals("audio"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendAudioAsync(chatId, file, caption);
            }
        }
        public void UploadFile(Telegram.Bot.TelegramBotClient bot, string destinationId, string filePath, string caption = null)
        {
            string fileFormat = FileFormat(filePath);
            if (fileFormat.Equals("picture"))
            {

                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendPhotoAsync(destinationId, file, caption);
            }
            else if (fileFormat.Equals("video"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendVideoAsync(destinationId, file, default, default, default, caption);
            }
            else if (fileFormat.Equals("file"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendDocumentAsync(destinationId, file, caption);
            }
            else if (fileFormat.Equals("audio"))
            {
                Stream stream = File.OpenRead(filePath);
                InputOnlineFile file = new InputOnlineFile(stream);
                bot.SendAudioAsync(destinationId, file, caption);
            }
        }
    }
}