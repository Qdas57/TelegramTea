using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp;
using ConsoleApp.Data.Entities;

namespace ConsoleApp
{
        public class PhotoContext : DbContext
        {
            public PhotoContext()
            {
                Database.EnsureCreated();
            }
            public DbSet<PhotoData> Photos { get; set; }
        
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=Asd;Database=aboba;Trusted_Connection=True;");

 
    }
}
