using System;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
    class Program
    {
        //TODO:
        //2. Я хочу по тегу получать фото, которое имеет этот тег, если фото несколько - случайную.
        //3. Добавить логику добавления фото в бд
        //
        //Паттерны проектирования: репозиторий.

        static void Main(string[] args)
        {
            TelegramLogics t = new TelegramLogics();
            t.Telegram(args);
        }
    }
}




