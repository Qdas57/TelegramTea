using System;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
    class Program
    {
        //Паттерны проектирования: репозиторий.

        static void Main(string[] args)
        {
            TelegramLogics t = new TelegramLogics();
            t.Telegram(args);
        }
    }
}




