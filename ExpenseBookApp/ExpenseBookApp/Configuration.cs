using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ExpenseBookApp
{
    static class Configuration
    {
        private static string Host { get; }
        private static string Port { get; }
        private static string DataBase { get; }
        private static string UserName { get; }
        private static string Password { get; }
        internal static DbContextOptions<ApplicationContext> Options { get; }

        static Configuration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            try
            {
                builder.AddJsonFile("AppSetings.json");
                var config = builder.Build();
                Host = config.GetConnectionString("Host");
                Port = config.GetConnectionString("Port");
                DataBase = config.GetConnectionString("DataBase");
                UserName = config.GetConnectionString("UserName");
                Password = config.GetConnectionString("Password");
                Options = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseNpgsql($"Host={Host};Port={Port};Database={DataBase};Username={UserName};Password={Password}").Options;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void Status()
        {
            Console.WriteLine("Работа в БД со следующими параметрами:");
            Console.WriteLine($"\tHost    -  {Host}");
            Console.WriteLine($"\tPort    -  {Port}");
            Console.WriteLine($"\tDataBase - {DataBase}");
            Console.WriteLine($"\tUserName - {UserName}");
            Console.WriteLine($"\tPassword - {Password}\n\n");
        }
    }
}
