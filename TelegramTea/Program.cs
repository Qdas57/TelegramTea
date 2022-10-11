using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           /* PhotoContext photoContext = new PhotoContext();
            var updateData = photoContext.Photos.SingleOrDefault(x => x.Name == "Some name");
            if (updateData != null )
            {
                photoContext.Update(updateData);
                photoContext.SaveChanges();
            }
            List<PhotoData> result = photoContext.Photos.ToListAsync().Result;
            foreach (var item in result)
            {
                Console.WriteLine($"{updateData.Id} \t {updateData.Name} \t {updateData.Path}");
            }*/
            TelegramLogics.Telegram(args);


        }

    }
}




