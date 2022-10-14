using System;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
    class Program
    {
        //TODO:
        //1. Добавить тег к photoEntity
        //2. Я хочу по тегу получать фото, которое имеет этот тег, если фото несколько - случайную.
        //3. Добавить логику добавления фото в бд
        //
        //Паттерны проектирования: репозиторий.

        static void Main(string[] args)
        {
            
            TelegramLogics.Telegram(args);
            
        }

    }
}




