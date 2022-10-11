using System;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PhotoContext photoContext = new PhotoContext();
            
            photoContext.Photos
                .AddAsync(new PhotoData
                {
                    DateUpload = DateTime.Now.ToString(),
                    NamePhoto = "meme.jpg"
                });

            photoContext.SaveChanges();

            TelegramLogics.Telegram(args);


        }

    }
}




