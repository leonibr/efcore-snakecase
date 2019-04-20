using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleConsoleApp.Data
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PersonalObject> Belongings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=database.db",
                opt =>
                {
                    // ::optional::
                    // the only table can´t be done by the extension is the migration history table
                    // sinces this part is database provider specific implementation
                    opt.MigrationsHistoryTable("__ef_migration_history");

                }).ReplaceService<IHistoryRepository, HistoryRepository>() ; // <!--- Specific implementation
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This is the only line you need to add in your context
            modelBuilder.ToSnakeCase();
        }
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string PersonalEmail { get; set; }
        public int AverageRate { get; set; }
        public List<PersonalObject> Belongings { get; set; }
    }

    public class PersonalObject
    {
        public int PersonalObjectId { get; set; }
        public string PersonalObjectTitle { get; set; }
        public string PersonalObjectContent { get; set; }

        public int PersonId { get; set; }
        public Person Owner { get; set; }
    }
}
