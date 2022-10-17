using Microsoft.EntityFrameworkCore;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
    public class PhotoContext : DbContext
        {
            public PhotoContext()
            {
                Database.EnsureCreated();
            }

            public DbSet<PhotoEntity> Photos { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseNpgsql("Server=Asd;Database=aboba;Trusted_Connection=True;");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Aboba;Trusted_Connection=True;");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=COMPUTER;Database=TgBot;Trusted_Connection=True;");
        //}
    }
}
