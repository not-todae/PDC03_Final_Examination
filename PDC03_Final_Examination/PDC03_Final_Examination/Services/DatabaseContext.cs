using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using PDC03_Final_Examination.Model;
using System.IO;

using Xamarin.Forms;

namespace PDC03_Final_Examination.Services
{
    public class DatabaseContext:DbContext
    {
        public DbSet<AnimalModel> Animals { get; set; }
        public DatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Animal.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
