using Microsoft.EntityFrameworkCore;
using ConsoleApp.Data.Entities;
using TelegramTea.Data.Enities;

namespace ConsoleApp
{
    public class PhotoContext : DbContext
    {
        public PhotoContext()
        {            
            Database.EnsureCreated();
        }

        public DbSet<PhotoEntity> Photos { get; set; }

        public DbSet<RequestEntity> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Aboba;Trusted_Connection=True;");
        }
    }
}
