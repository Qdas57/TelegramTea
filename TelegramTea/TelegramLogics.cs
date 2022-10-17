using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using ConsoleApp.Data.Entities;
using TelegramTea.Repositories;
using Telegram.Bot.Types.InputFiles;
using System.Runtime.ConstrainedExecution;

namespace ConsoleApp
{
    //S.O.L.I.D - 

    //S - принцип единственной ответственности - Single responsibility principle
    // Паттерн проектирования Репозиторий
    internal class TelegramLogics
    {
        private readonly PhotoRepository _photoRepository;

        public TelegramLogics()
        {
            _photoRepository = new PhotoRepository();
        }

        public async Task Telegram(string[] args)
        {
            var client = new TelegramBotClient("5641621844:AAEdBTO7vQphtQhg9Gbx7dKYxYpf6aYKoLs");
            client.StartReceiving(Update, Error);            

            Console.WriteLine("The coolest logs in thread..");
            Console.ReadLine();
        }

        async Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
        }

        async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (!string.IsNullOrWhiteSpace(message.Text))        
            {
                Console.WriteLine($"Data: {DateTime.Now}\n ChatId: {message.Chat.Id} \n Action: send message {message.Text}");
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Hiiii, {message.Chat.FirstName} \nJust send me Photo(document)");
                    return;
                }
                if (message.Text.ToLower() == "/tagfinder")
                { 
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Send me tag {message.Chat.FirstName}");
                    return;

                }
                await botClient.SendTextMessageAsync(message.Chat.Id, $"I don't understand you, {message.Chat.FirstName}");
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

                var imageName = $"{Guid.NewGuid()}.jpg";
                var directPath = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{message.Chat.Id}");
                string destinationFilePath = $@"{directPath}\{imageName}";

                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                await botClient.DownloadFileAsync(filePath, fileStream);

                var tag = string.IsNullOrWhiteSpace(message.Caption) ? "Без тега" : message.Caption;

                var createdPhoto = _photoRepository.CreatePhoto(destinationFilePath, tag);

                Console.WriteLine($"Файл успешно сохранен с новым именем: {imageName}");
                Console.WriteLine($"Тег фото: {tag}");
                
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Файл сохранен с тегом: {tag}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Файл успешно сохранен с новым именем: {imageName}");

                Thread.Sleep(10000);
                await using Stream stream = System.IO.File.OpenRead(destinationFilePath);
                await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(stream,imageName), tag);

                return;
            }
            
            // blob
            // x => x.Name
            // (parameters) => x.Name == "Some string"
            // LINQ, лямбды, анонимные методы


            //VS, VS Code, Rider
            //ReSharper -> 

            //Роберт Мартин "Чистый Код"
        }
    }
}