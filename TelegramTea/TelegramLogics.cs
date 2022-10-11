using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;
using ConsoleApp;
using System.Linq;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace ConsoleApp
{
    internal class TelegramLogics
    {
        DateTime dateTime = new DateTime();
        public static async Task Telegram(string[] args)
        {
            var client = new TelegramBotClient("5641621844:AAEdBTO7vQphtQhg9Gbx7dKYxYpf6aYKoLs");
            client.StartReceiving(Update, Error);
            Console.WriteLine("The coolest logs in thread..");
            Console.ReadLine();

        }

        async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                Console.WriteLine($"Data: {DateTime.Now}\n ChatId: {message.Chat.Id} \n Action: send message {message.Text}");
                if (message.Text.ToLower().Contains("/start"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Hiiii, {message.Chat.FirstName} \nJust send me Photo(document)");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Я пока не могу распознать это сообщение :(");
                return;
            }
            if (message.Photo != null)
            {
                Console.WriteLine($"Data: {DateTime.Now}\n ChatId: {message.Chat.Id} \n Action: send Photo {message.Photo}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Почти угадал с форматом! Трайни ещё разок");
                return;
            }
            if (message.Document != null)
            {
                Console.WriteLine($"Data: {DateTime.Now}\n ChatId: {message.Chat.Id} \n Action: send Document {message.Document.FileName}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Перехожу к записи в бд");

                var fileId = update.Message.Document.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                string destinationFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{Guid.NewGuid()}.jpg";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                await botClient.DownloadFileAsync(filePath, fileStream);
                Console.WriteLine("Файл успешно сохранен с новым именем: " + Guid.NewGuid());
                await botClient.SendTextMessageAsync(message.Chat.Id, "Файл успешно сохранен с новым именем: " + Guid.NewGuid());
                return;
            }

        }
    }
}
