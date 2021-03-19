using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ExpenseBookApp
{
    internal static class AppFunctioality
    {
        //----------------------------------------Work with Person--------------------------------------------------
        
        // Находит и возвращает искомый объект (Person) по Имени и Фамилии
        internal static Person FindPerson(string firstName, string lastName)
        {

            Person person = null;

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    person = db.Persons.FirstOrDefault(per => per.FirstName == firstName && per.LastName == lastName);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }

            return person;
        }

        // Находит и возвращает искомый объект (Person) по Имени и Фамилии
        internal static Person FindPerson(int idPerson)
        {
            Person person = null;

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    person = db.Persons.FirstOrDefault(per => per.PersonId == idPerson);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }

            return person;
        }

        // Показ всех зарегистрированных Person
        internal static void ToPrintPerson()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    var persons = db.Persons.ToList();
                    Console.WriteLine($" Id\t\tFirst\t\t\tLastName");
                    Console.WriteLine($"-------+-------------------------+---------------------+");
                    foreach (Person pers in persons)
                    {
                        Console.WriteLine($" {pers.PersonId}\t\t{pers.FirstName}   \t\t{pers.LastName}");
                        Console.WriteLine($"-------+-------------------------+---------------------+");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Добавление нового Person
        internal static void AddPerson(string firstName, string lastName)
        {
            Person person = new Person { FirstName = firstName, LastName = lastName };

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    db.Persons.Add(person);
                    db.SaveChanges();
                }
                Console.WriteLine($"Добавлен новый пользователь: {firstName} {lastName}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Удаление Person
        internal static void RemovePerson(Person person)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    db.Persons.Remove(person);
                    db.SaveChanges();
                }
                Console.WriteLine($"Пользователь {person.FirstName} {person.LastName} удален из БД!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }


        //----------------------------------------Work with Expenditure------------------------------------------------------------
        // Добавления новой траты к Person
        internal static void AddExpenditure(int personId, char category, decimal sum, DateTime time)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    Person person = db.Persons.First(per => per.PersonId == personId);
                    Expenditure expenditure = new Expenditure { Category = category, TotalSum = sum, Date = time, Person = person };
                    db.Expenses.Add(expenditure);
                    db.SaveChanges();
                    Console.WriteLine($"Добавленна новая трата: {time} - {sum} к {person.FirstName} {person.LastName}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Поиск трат по дате. Находится подходящая трата и возвращает его ID
        internal static int FindExpenditure(DateTime date, decimal sum)
        {
            Expenditure expenditure = null;

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    expenditure = db.Expenses.FirstOrDefault(expend => expend.Date == date && expend.TotalSum == sum);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }

            return expenditure.ExpenditureId;
        }

        // Поиск траты по ее Id.
        internal static Expenditure FindExpenditure(Person person,int idExpenditure)
        {
            Expenditure expenditure = null;

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    expenditure = db.Expenses.Include(ex=>ex.Person).
                        FirstOrDefault(exp => exp.ExpenditureId == idExpenditure && exp.Person.PersonId == person.PersonId);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }

            return expenditure;
        }

        // Удаляем трату
        internal static void RemoveExpenditure(Expenditure expenditure)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    db.Expenses.Remove(expenditure);
                    db.SaveChanges();
                }
                Console.WriteLine($"Трата {expenditure.ExpenditureId} {expenditure.Date} пользователя {expenditure.Person.FirstName} {expenditure.Person.LastName} удалена из БД!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Изменяем трату
        internal static void ChangeExpenditure(Expenditure expenditure, char category, decimal sum, DateTime date)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    expenditure.Category = category;
                    expenditure.TotalSum = sum;
                    expenditure.Date = date;
                    db.Expenses.Update(expenditure);
                    db.SaveChanges();
                }
                Console.WriteLine($"Трата пользователя {expenditure.Person.FirstName} {expenditure.Person.LastName} изменена.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }


        //----------------------------------------Work with Income-----------------------------------------------------------------
        // Добавления нового постулпения к Person
        internal static void AddIncome(int personId, decimal sum, DateTime time)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    Person person = db.Persons.First(per => per.PersonId == personId);
                    Income income = new Income { SumAdd = sum, DateAdd = time, Person = person };
                    db.Incomes.Add(income);
                    db.SaveChanges();
                    Console.WriteLine($"Добавлено новое поступление: {time} - {sum} к {person.FirstName} {person.LastName}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Поиск поступлений по Id
        internal static Income FindIncome(Person person, int idIncome)
        {
            Income income = null;

            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    income = db.Incomes.Include(ex => ex.Person).
                        FirstOrDefault(exp => exp.IncomeId == idIncome && exp.Person.PersonId == person.PersonId);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }

            return income;
        }

        // Удаление поступлений
        internal static void RemoveIncome(Income income)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    db.Incomes.Remove(income);
                    db.SaveChanges();
                }
                Console.WriteLine($"Поступление {income.IncomeId} {income.DateAdd} пользователя {income.Person.FirstName} {income.Person.LastName} удалено из БД!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Изменяем поступление
        internal static void ChangeIncome(Income income, decimal sum, DateTime date)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    income.DateAdd = date;
                    income.SumAdd = sum;
                    db.Incomes.Update(income);
                    db.SaveChanges();
                }
                Console.WriteLine($"Поступление пользователя {income.Person.FirstName} {income.Person.LastName} изменено.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        //----------------------------------------Work with Product---------------------------------------------------------------
        // Добавление нового продукта к Expenditure
        internal static void AddProduct(int expenditureId, string productName, decimal cost, int count)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    Expenditure expenditure = db.Expenses.First(exp => exp.ExpenditureId == expenditureId);
                    Product product = new Product { ProductName = productName, ProductCost = cost, ProductCount = count, Expenses = expenditure };
                    db.Products.Add(product);
                    db.SaveChanges();
                    Console.WriteLine($"Добавленна новый продукт: {productName} - стоимостью  {cost}  в коллиечтве {count} к тратам {expenditure.Date}  - {expenditure.TotalSum}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Проссмотр всех продкутов
        internal static void ToPrintProduct()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    var expenses = db.Products.Include(ex => ex.Expenses).ThenInclude(per => per.Person).ToList();

                    Console.WriteLine("-------------------------------------Product-------------------------------------------------------");
                    foreach (Product product in expenses)
                    {
                        Console.WriteLine($"{product.ProductId}\t{product.ProductName}  \tcount:{product.ProductCount}\tcost:{product.ProductCost}" +
                            $"\tis included for Expenses: Id - {product.Expenses.ExpenditureId} Sum - {product.Expenses.TotalSum} for {product.Expenses.Person.FirstName} {product.Expenses.Person.LastName}");
                        Console.WriteLine($"------------------------------------------------------------------------------------------");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Проссмотр всех продуктов для заданной траты
        internal static void ToPrintProduct(Expenditure expenditure)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    var expenses = db.Products.Include(ex => ex.Expenses).ThenInclude(per => per.Person).Where(ex=>ex.Expenses == expenditure).ToList();

                    Console.WriteLine("-------------------------------------Product-------------------------------------------------------");
                    foreach (Product product in expenses)
                    {
                        Console.WriteLine($"{product.ProductId}\t{product.ProductName}  \tcount:{product.ProductCount}\tcost:{product.ProductCost}" +
                            $"\tis included for Expenses: Id - {product.Expenses.ExpenditureId} Sum - {product.Expenses.TotalSum} for {product.Expenses.Person.FirstName} {product.Expenses.Person.LastName}");
                        Console.WriteLine($"------------------------------------------------------------------------------------------");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        //---------------------------------------------ToPrintExpenditure----------------------------------------------------------

        // Показ трат всех зарегистрированых Person
        internal static void ToPrintExpenditure()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    var expenses = db.Expenses.Include(ex => ex.Person).ToList();

                    Console.WriteLine("--------------------Expenditure---------------------------------");
                    foreach (Expenditure ex in expenses)
                    {
                        Console.WriteLine($"{ex.ExpenditureId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.TotalSum} || {ex.Date}");
                        Console.WriteLine($"---------------------------------------------");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Показ всех трат опредленным Person
        internal static void ToPrintExpenditure(Person person)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    if (person == null)
                        Console.WriteLine("Данного пользователя нет в БД!");
                    else
                    {
                        var expenses = db.Expenses.Include(ex => ex.Person).ToList()
                            .Where(ex => ex.Person.LastName == person.LastName && ex.Person.FirstName == person.FirstName);

                        Console.WriteLine("--------------------Expenditure---------------------------------");
                        foreach (Expenditure ex in expenses)
                        {
                            Console.WriteLine($"{ex.ExpenditureId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.TotalSum} || {ex.Date}");
                            Console.WriteLine($"---------------------------------------------");
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Показ всех трат определенным Person в соотвествующий месяц(yaer, mounth)
        internal static void ToPrintExpenditure(Person person, int year, int mounth)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    if (person == null)
                        Console.WriteLine("Данного пользователя нет в БД!");
                    else
                    {
                        var expenses = db.Expenses.Include(ex => ex.Person).ToList()
                            .Where(ex => ex.Person.LastName == person.LastName && ex.Person.FirstName == person.FirstName && ex.Date.Year == year && ex.Date.Month == mounth);

                        Console.WriteLine("--------------------Expenditure---------------------------------");
                        foreach (Expenditure ex in expenses)
                        {
                            Console.WriteLine($"{ex.ExpenditureId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.TotalSum} || {ex.Date}");
                            Console.WriteLine($"---------------------------------------------");
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        //-------------------------------------------ToPrintIncome---------------------------------------------------

        // Показ всех пополнений зарегистрированых Person
        internal static void ToPrintIncome()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    var expenses = db.Incomes.Include(ex => ex.Person).ToList();
                    Console.WriteLine("--------------------Income---------------------------------");
                    foreach (Income ex in expenses)
                    {
                        Console.WriteLine($"{ex.IncomeId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.SumAdd} || {ex.DateAdd}");
                        Console.WriteLine($"---------------------------------------------");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Показ пополнений опредленным Person
        internal static void ToPrintIncome(Person person)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    if (person == null)
                        Console.WriteLine("Данного пользователя нет в БД!");
                    else
                    {
                        var expenses = db.Incomes.Include(ex => ex.Person).ToList()
                            .Where(ex => ex.Person.LastName == person.LastName && ex.Person.FirstName == person.FirstName);

                        Console.WriteLine("--------------------Income---------------------------------");
                        foreach (Income ex in expenses)
                        {
                            Console.WriteLine($"{ex.IncomeId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.SumAdd} || {ex.DateAdd}");
                            Console.WriteLine($"---------------------------------------------");
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }

        // Показ всех пополнений определенным Person в соотвествующий месяц(yaer, mounth)
        internal static void ToPrintIncome(Person person, int year, int mounth)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext(Configuration.Options))
                {
                    if (person == null)
                        Console.WriteLine("Данного пользователя нет в БД!");
                    else
                    {
                        var expenses = db.Incomes.Include(ex => ex.Person).ToList()
                            .Where(ex => ex.Person.LastName == person.LastName && ex.Person.FirstName == person.FirstName && ex.DateAdd.Year == year && ex.DateAdd.Month == mounth);

                        Console.WriteLine("--------------------Income---------------------------------");
                        foreach (Income ex in expenses)
                        {
                            Console.WriteLine($"{ex.IncomeId} {ex.Person.FirstName} {ex.Person.LastName}  - {ex.SumAdd} || {ex.DateAdd}");
                            Console.WriteLine($"---------------------------------------------");
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"#ERROR: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредевиденная ошибка: {ex.GetType()}");
            }
        }
    }
}
