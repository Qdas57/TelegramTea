using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using TelegramTea.Repositories;
using Telegram.Bot.Types.InputFiles;

namespace ConsoleApp
{
    //S.O.L.I.D - 

    //S - принцип единственной ответственности - Single responsibility principle
    

    //TODO: вынести логику для работы с файловой системой в отдельный класс - FileService ??

    internal class TelegramLogics
    {
        private readonly PhotoRepository _photoRepository;

        private readonly string pathToRootDir = @"g:\PROFI.PROJECTS\NET\TelegramTea\Content";

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
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Hi, {message.Chat.FirstName} \nJust send me Photo(document)");
                    return;
                }
                if (message.Text.ToLower() == "/help")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Hi! Сommands to work with me: \n /start - Запуск бота \n /help - Список команд \n /random - Случайная фотография \n /tag - Все доступные теги( без повторов ) \n Отправьте мне фото документом и подпишите его - после этого я запишу его к себе в бд! Так же ты можешь написать произвольный тег и я попробую его найти!");
                    return;
                }
                if (message.Text.ToLower() == "/tags")
                {                   
                    var tags = _photoRepository.GetTagList();
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Available tags: {string.Join(", ", tags)}");

                    return;
                }
                if (message.Text.ToLower() == "/random")
                {
                    try
                    {
                        var randomPhoto = _photoRepository.GetRandomPhoto();

                        await using Stream stream = System.IO.File.OpenRead(randomPhoto.NamePhoto);

                        await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(stream), randomPhoto.Tag);

                        return;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message + "Ошибка в /random");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Send this message to admin: " + e.Message);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        var randomPhoto = _photoRepository.GetRandomPhotoByTag(message.Text);

                        if (randomPhoto is null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"Photo with tag {message.Text} not found.");

                            return;
                        }

                        await using Stream stream = System.IO.File.OpenRead(randomPhoto.NamePhoto);

                        await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(stream), randomPhoto.Tag);
                    }
                    catch (Exception e)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Send this message to admin: " + e.Message);
                        Console.WriteLine(e.Message);
                        return;
                    }
                    
                }

                return;
                
            }
            if (message.Photo != null)
            {
                Console.WriteLine($"Data: {DateTime.Now}\n ChatId: {message.Chat.Id} \n Action: send Document {message.Photo}");

                await botClient.SendTextMessageAsync(message.Chat.Id, $"Перехожу к записи в бд");

                var fileId = update.Message.Photo[update.Message.Photo.Length - 1].FileId;
                                
                var fileInfo = await botClient.GetFileAsync(fileId);

                //???
                var filePath = fileInfo.FilePath;

                var imageName = $"{Guid.NewGuid()}.jpg";
                
                var directPath = Directory.CreateDirectory($@"{pathToRootDir}\{message.Chat.Id}");
                string destinationFilePath = $@"{directPath}\{imageName}";

                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);

                await botClient.DownloadFileAsync(filePath, fileStream);

                //

                var tag = string.IsNullOrWhiteSpace(message.Caption) ? "Без тега" : message.Caption;

                var createdPhoto = _photoRepository.CreatePhoto(destinationFilePath, tag);

                Console.WriteLine($"Файл успешно сохранен с новым именем: {imageName}");
                Console.WriteLine($"Тег фото: {tag}");

                await botClient.SendTextMessageAsync(message.Chat.Id, $"Файл сохранен с тегом: {tag}");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Файл успешно сохранен с новым именем: {imageName}");
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

                //Thread.Sleep(10000);



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