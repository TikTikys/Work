using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace ExpenseBookApp
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Expenditure> Expenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.\nОшибка подключения к серверу. Проверьте конфигурационный файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"#ERROR: {ex.GetType()}.\n{ex.Message}.");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expenditure>()
                .HasOne(exp => exp.Person)
                .WithMany(pers => pers.Exceptions)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(prod => prod.Expenses)
                .WithMany(exp => exp.Products)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Income>()
                .HasOne(inc => inc.Person)
                .WithMany(pers => pers.Incomes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
