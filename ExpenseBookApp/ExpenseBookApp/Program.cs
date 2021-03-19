using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Configuration;


namespace ExpenseBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в приложение по учету расходов и поступлений.");

            // Считываение конфигурациронного файла с помощью статического констурктора и вывод на экран его параметров
            Configuration.Status();


            // Запуск приложения (консольная версия) => Планировал\нию заменить классом описывающим поведение с помощью WFP (Вышла накладка ибо читал про WinForm, а он в Core не поддерживается)
            ConsoleApp.Start();

            Console.WriteLine("Конец работы в приложении");
            Console.ReadKey();

        }
    }
}
