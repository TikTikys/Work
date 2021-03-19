using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace ExpenseBookApp
{
    internal static class ConsoleApp
    {
        // Начальный метод. Точка отсчета
        internal static void Start()
        {
            Console.WriteLine("Добро пожаловать в приложение по учету доходов и рассходов.");
            bool flag = true;

            while (flag)
            {
                StartWindowConsole();
                Console.WriteLine("Открытие справочного окна с командами - \"help\"");
                switch (Console.ReadLine())
                {
                    case "1":
                        PersonWindowConsole();                                      // Метод для работы с пользовательскими данными
                        break;
                    case "2":
                        ExpendOrIncomeConsole();                                    // Добавление новых рассходов и трат 
                        break;
                    case "3":
                        ChangeOrDeleteWindowConsole();                              // Поиск и корректировка данных
                        break;
                    case "help":
                        StartWindowConsole();
                        break;
                    case "4":
                        flag = false;                                               // Окончание работы в приложнии 
                        break;
                    default:
                        Console.WriteLine("Данной операции не существует.");
                        break;
                }
            }
            Console.WriteLine("Конец работы приложения");
        }

        // ------------------------Методы первой ветви (Работа с пользовательскими данныими)-------------------------------

        // Работа с пользователями. Ветвь - 1
        private static void PersonWindowConsole()
        {
            bool flag = true;
            do
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Проссмотр баланса зарегистрированных пользователей
                                        2. Зарегистрировать нового пользователя
                                        3. Удаление зарегистрированного пользователя
                                        4. Проссмотр всех зарегистрированных пользователей
                                        5. Назад");

                switch (Console.ReadLine())
                {
                    case "1":
                        flag = false;
                        FindBalanceConsole();                                           // Метод проссмотра баланса на счету
                        break;
                    case "2":
                        AddNewPerson();                                                 // Метод добавления нового пользователя
                        break;
                    case "3":
                        DeletePerson();                                                 // Метод удалениея сещуствующего пользователя (через поиск)
                        break;
                    case "4":
                        AppFunctioality.ToPrintPerson();                                // Метод показывающий всех зарегистрированных пользователей
                        break;
                    case "5":
                        flag = false;                                                  // Возращаемся на гланую страницу.
                        break;
                    default:
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        // Метод проссмотра баланса пользователей. Ветвь - 1.1
        private static void FindBalanceConsole()
        {
            bool flag = true;
            do
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Проссмотр трат определенного пользователя
                                        2. Проссмотр трат всех пользователей
                                        3. Проссмотр пополнения определенного пользователя
                                        4. Проссмотр пополнений всех пользователей
                                        5. Назад");

                switch (Console.ReadLine())
                {
                    case "1":
                        flag = false;
                        PrintPersonExpenditureConsole();                                        // Открытие окна трат пользователя
                        break;
                    case "2":
                        AppFunctioality.ToPrintExpenditure();                                   // Вывод на экран трат всех пользователй
                        break;
                    case "3":
                        flag = false;
                        PringPersonIncomeConsole();                                             // Метод открытия окна пополнения пользоваетля
                        break;
                    case "4":
                        AppFunctioality.ToPrintIncome();                                        // Вывод на экран пополнений всех пользователей
                        break;
                    case "5":
                        flag = false;
                        PersonWindowConsole();                                                  // Возвращаемся к выбору опций с работой пользователей
                        break;
                    default:
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        // Метод для проссмотра трат. Конечная ветвь - 1.1.1
        private static void PrintPersonExpenditureConsole()
        {
            bool flag = true;
            do
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Проссмотр трат пользователя за все время
                                        2. Проссмотр трат пользователя за определенный месяц
                                        3. Вернуться на главный экран
                                        4. Назад");

                switch (Console.ReadLine())
                {
                    case "1":
                        AppFunctioality.ToPrintExpenditure(FindPerson());                               // Проссмотр трат пользователя за все время
                        break;
                    case "2":
                        var date = ImputDate();
                        AppFunctioality.ToPrintExpenditure(FindPerson(), date.year, date.mounth);       // Проссмотр трат пользователя за определенный месяц
                        break;
                    case "3":
                        flag = false;
                        StartWindowConsole();                                                           // Закрываем все методы возвращаемся в главный метод. Также вызывается справочный метод
                        break;
                    case "4":
                        flag = false;
                        FindBalanceConsole();                                                                  // Возвращаемся к методу проссмотра баланса пользователя
                        break;
                    default:
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        // Метод для проссмотра поступлений. Конечная ветвь - 1.1.1
        private static void PringPersonIncomeConsole()
        {
            bool flag = true;
            do
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Проссмотр пополнений пользователя за все время
                                        2. Проссмотр пополнений пользователя за определенный месяц
                                        3. Вернуться на главный экран
                                        4. Назад");

                switch (Console.ReadLine())
                {
                    case "1":
                        AppFunctioality.ToPrintIncome(FindPerson());                               // Проссмотр трат пользователя за все время
                        break;
                    case "2":
                        var date = ImputDate();
                        AppFunctioality.ToPrintIncome(FindPerson(), date.year, date.mounth);       // Проссмотр трат пользователя за определенный месяц
                        break;
                    case "3":
                        flag = false;
                        StartWindowConsole();                                                       // Закрываем все методы возвращаемся в главный метод. Также вызывается справочный метод
                        break;
                    case "4":
                        flag = false;
                        FindBalanceConsole();                                                              // Возвращаемся к методу проссмотра баланса пользователя
                        break;
                    default:
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        // Метод для регистрации нового пользователя
        private static void AddNewPerson()
        {
            Console.Write("Введите имя и фамилию пользователя которого хотите ДОБАВИТЬ.\nИх сочетание должно быть УНИКАЛЬНЫМ!!!.\nFirst Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            if (AppFunctioality.FindPerson(firstName, lastName) == null)
                AppFunctioality.AddPerson(firstName, lastName);
            else
                Console.WriteLine("Данный пользователь уже занесен в базу данных!");
        }

        // Метод для удаления зарегистрированного пользователя
        private static void DeletePerson()
        {
            Console.Write("Введите имя и фамилию пользователя которого хотите УДАЛИТЬ.\nFirst Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Person person = AppFunctioality.FindPerson(firstName, lastName);
            if (person == null)
                Console.WriteLine("Данного пользователя нет в БД!");
            else
                AppFunctioality.RemovePerson(person);
        }

        //-------------------------Методы второй ветви (Добавление новых трат и поступлений)-----------------------------

        // Работа с тартами и постулпениями. Ветвь 2
        private static void ExpendOrIncomeConsole()
        {
            bool flag = false;
            do
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Добавить новые траты
                                        2. Добавить новые поступления
                                        3. Проссмотр всех купленных продуктов
                                        4. Назад");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewExpenditure();                                                // Метод добалвения новых трат
                        break;
                    case "2":
                        AddNewIncome();                                                     // Метод добавления новых поступлений
                        break;
                    case "3":
                        AppFunctioality.ToPrintProduct();                                   // Проссмотр всех продкутов
                        break;
                    case "4":
                        break;
                    default:
                        flag = true;
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        // Метод добавления новых поступлений
        private static void AddNewIncome()
        {
            Console.WriteLine("Выполняет добавление поступлений.\nНа данный момент зарегистрированы следующие пользователи");
            Person person = ChoicePerson();                                                 // Метод выбора Person для добавления новых постулпений
            if (person != null)
            {
                DateTime date = ImputDateFull();
                decimal sum;
                bool flag;
                do
                {
                    Console.Write("Введите положенную сумму: ");
                    flag = !Decimal.TryParse(Console.ReadLine(),out sum);
                    if (flag)
                        Console.WriteLine("Ошибка ввода суммы поступления");
                } while (flag);
                AppFunctioality.AddIncome(person.PersonId, sum, date);
            }
            else
                Console.WriteLine("Пользователя с таким Id не обнаруженно");
        }

        // Метод добалвения новых трат
        private static void AddNewExpenditure()
        {
            Console.WriteLine("Выполняет добавление трат.\nНа данный момент зарегистрированы следующие пользователи: ");
            Person person = ChoicePerson();                                                 // Метод выбора Person для добавления новых трат
            if (person != null)
            {
                DateTime date = ImputDateFull();
                decimal sum;
                bool flag;
                char category;
                do
                {
                    Console.Write("Введите сумму затраченных средств: ");
                    flag = Decimal.TryParse(Console.ReadLine(), out sum);
                    Console.Write("введите категирию товара(Пример: 'A' или '$'): ");
                    flag &= Char.TryParse(Console.ReadLine(), out category);
                    if (!flag)
                        Console.WriteLine("Ошибка ввода суммы тарат или катерии товара");
                } while (!flag);
                AppFunctioality.AddExpenditure(person.PersonId, category, sum, date);
                AddProducts(date, sum);                                                     // Рассписывает траты
            }
            else
                Console.WriteLine("Пользователя с таким Id не обнаруженно");
        }

        // Рассписание трат, добавление товаров
        private static void AddProducts(DateTime date, decimal sum)
        {
            Console.WriteLine("Рассписываем траты.");
            int expenditureId = AppFunctioality.FindExpenditure(date, sum);
            bool flagParse;
            string productName;
            int productCount;
            decimal productCost;
            do
            {
                Console.WriteLine($"\n\nОсталось расспределить {sum} единиц трат.");
                Console.Write("\nВведите название продукта: ");
                productName = Console.ReadLine();
                Console.Write("Введите его стоимость");
                flagParse = Decimal.TryParse(Console.ReadLine(), out productCost);
                Console.Write("Введите его коллиество: ");
                flagParse &= Int32.TryParse(Console.ReadLine(), out productCount);
                if (flagParse)
                {
                    if (productCost * productCount <= sum)
                    {
                        AppFunctioality.AddProduct(expenditureId, productName, productCost, productCount);
                        sum -= productCost * productCount;
                    }
                    else
                        Console.WriteLine("Ошибка.\nСумма стоимости товаров больше трат");
                }
                else
                    Console.WriteLine("Некорректный ввод цены или коллества купленных товаров");
            } while (sum > 0.001m);
        }

        //-------------------------Методы третьей ветви (Корректировка трат и поступлений)-------------------------------
        // Корректировка и удаление данных пользователя

        private static void ChangeOrDeleteWindowConsole()
        {
            bool flag = true;
            do 
            {
                Console.WriteLine(@"Выберете следующее действие:
                                        1. Корректировать траты
                                        2. Корректировать постулпения
                                        3. Назад");
                switch (Console.ReadLine())
                {
                    case "1":
                        ChangeOrDeleteExpediture();                                             // Вызываем метод для работы с тратами Person
                        break;
                    case "2":
                        ChangeOrDeleteIncome();                                                 // Вызываем метод для работы с постулпениями Person
                        break;
                    case "3":
                        flag = false;                                                           // Заканчиваем работу в цикле.
                        break;
                    default:
                        Console.WriteLine("Введенно некорректное значение");
                        break;
                }
            } while (flag);
        }

        private static void ChangeOrDeleteExpediture()
        {
            Console.WriteLine("Выполняется корректировка трат.\nНа данный момент зарегистрированы следующие пользователи: ");
            Person person = ChoicePerson();
            if (person != null)
            {
                Console.WriteLine($"Выбран пользователь: {person.FirstName} {person.LastName}" +
                                 $"У данного пользователя есть следующие траты:");              
                Expenditure expediture = ChoiceExpenditure(person);
                if (expediture != null)
                {
                    bool flag = false;
                    do
                    {
                        Console.WriteLine(@"Выберете следующее действие:
                                                1.Корректировать трату
                                                2.Удалить трату
                                                3.Назад");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                ChangeExpenditure(expediture);                                                              // Корректировать трату
                                break;
                            case "2":
                                AppFunctioality.RemoveExpenditure(expediture);                                              // Удалить трату
                                break;
                            case "3":
                                break;
                            default:
                                flag = true;
                                Console.WriteLine("Введенно некорректное значение");
                                break;
                        }
                    } while (flag);
                }
                else
                    Console.WriteLine("Данной тараты у данного пользователя не обнаружено.");

            }
            else
                Console.WriteLine("Пользователя с таким Id не обнаруженно");
        }

        // Выбор траты
        private static Expenditure ChoiceExpenditure(Person person)
        {
            AppFunctioality.ToPrintExpenditure(person);
            
            int Id;
            Console.WriteLine("Выберете тарту - введите ее Id");
            while (!Int32.TryParse(Console.ReadLine(), out Id))
                Console.WriteLine("Введенно некрорректное значение Id");

            return AppFunctioality.FindExpenditure(person,Id);
        }

        // Корректировка траты
        private static void ChangeExpenditure(Expenditure expenditure)
        {
            Console.WriteLine($"\n\nТрата пользователя {expenditure.Person.FirstName} {expenditure.Person.LastName} имеет следующие параметры:\n" +
                $"Sum - {expenditure.TotalSum};\nDate - {expenditure.Date};\nCategory - {expenditure.Category};\n" +
                $"и содержит следующие продукты:");
            AppFunctioality.ToPrintProduct(expenditure); 

            decimal newSum;
            Console.WriteLine("Введите новое значение Sum:");
            while (!Decimal.TryParse(Console.ReadLine(), out newSum)) ;

            char newCat;
            Console.WriteLine("Введите новое значение категории товара:");
            while (!Char.TryParse(Console.ReadLine(), out newCat))

            Console.WriteLine("Выберете новое значение Date:");
            DateTime newDate = ImputDateFull();

            if (newSum != expenditure.TotalSum)
            {
                AppFunctioality.RemoveExpenditure(expenditure);

                AppFunctioality.AddExpenditure(expenditure.Person.PersonId, newCat, newSum, newDate);

                Console.WriteLine("Введите обновленные данные о товарах:");
                AddProducts(newDate, newSum);
            }
            else
                AppFunctioality.ChangeExpenditure(expenditure, newCat, newSum, newDate);

        }

        // Корректировка поступлений
        private static void ChangeOrDeleteIncome()
        {
            Console.WriteLine("Выполняется корректировка потуплений.\nНа данный момент зарегистрированы следующие пользователи: ");
            Person person = ChoicePerson();
            if (person != null)
            {
                Console.WriteLine($"Выбран пользователь: {person.FirstName} {person.LastName}" +
                                 $"У данного пользователя есть следующие поступления:");
                Income income = ChoiseIncome(person);
                if (income != null)
                {
                    bool flag = false;
                    do
                    {
                        Console.WriteLine(@"Выберете следующее действие:
                                                1.Корректировать трату
                                                2.Удалить трату
                                                3.Назад");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                ChangeIncome(income);                                                              // Корректировать трату
                                break;
                            case "2":
                                AppFunctioality.RemoveIncome(income);                                              // Удалить трату
                                break;
                            case "3":
                                break;
                            default:
                                flag = true;
                                Console.WriteLine("Введенно некорректное значение");
                                break;
                        }
                    } while (flag);
                }
                else
                    Console.WriteLine("Данной тараты у данного пользователя не обнаружено.");

            }
            else
                Console.WriteLine("Пользователя с таким Id не обнаруженно");
        }

        private static Income ChoiseIncome(Person person)
        {
            AppFunctioality.ToPrintIncome(person);

            int Id;
            Console.WriteLine("Выберете тарту - введите ее Id");
            while (!Int32.TryParse(Console.ReadLine(), out Id))
                Console.WriteLine("Введенно некрорректное значение Id");

            return AppFunctioality.FindIncome(person, Id);
        }


        // Корректировка поступления
        private static void ChangeIncome(Income income)
        {
            Console.WriteLine($"\n\nПоступление пользователя {income.Person.FirstName} {income.Person.LastName} имеет следующие параметры:\n" +
                $"Sum - {income.SumAdd};\nDate - {income.DateAdd};");

            decimal newSum;
            Console.WriteLine("Введите новое значение Sum:");
            while (!Decimal.TryParse(Console.ReadLine(), out newSum)) ;

            Console.WriteLine("Выберете новое значение даты поступления:");
            DateTime newDate = ImputDateFull();

            AppFunctioality.ChangeIncome(income, newSum, newDate);
        }

        //---------------------------Вспомогательные методы--------------------------------------------------------------

        // Работа в главном окне
        private static void StartWindowConsole()
        {
            Console.WriteLine(@"Выберете следующее действие:
                                    1. Работа с пользовательскими данными
                                    2. Добавление новых расходов/трат пользователей
                                    3. Поиск и корректировка данных пользователей
                                    4. Закрыть приложение");
        }

        // Метод поиск пользователя
        private static Person FindPerson()
        {
            Console.Write("Введите имя и фамилию интересующего пользователя.\nFirst Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            return AppFunctioality.FindPerson(firstName, lastName);
        }

        // Ввод месяца и года для поиска условного месяца в году
        private static (int year, int mounth) ImputDate()
        {
            Console.WriteLine("\nИдет ввод даты:");
            bool flag = true;
            var result = (year:0, mounth:0);
            do
            {
                Console.Write($"Введите год: ");
                bool bYear = Int32.TryParse(Console.ReadLine(), out int year);
                Console.Write($"Введите месяц: ");
                bool bMounth = Int32.TryParse(Console.ReadLine(), out int mounth);
                if (bYear && bMounth && mounth > 0 && mounth < 13)
                {
                    result.year = year;
                    result.mounth = mounth;
                    flag = false;
                }
                else
                    Console.WriteLine("Ошибка при вводе данных.\nПроводится повторный ввод даты.");
            }while (flag) ;

            return result;
        }

        private static DateTime ImputDateFull()
        {
            Console.WriteLine("\nИдет ввод даты:");
            DateTime date;
            string day, mounth, year;
            bool flag;
            do
            {
                Console.Write("Введите день: ");
                day = Console.ReadLine();
                Console.Write("Введите месяц: ");
                mounth = Console.ReadLine();
                Console.Write("Введите год: ");
                year = Console.ReadLine();
                flag = !DateTime.TryParse($"{day}/{mounth}/{year}", out date);
                if (flag)
                    Console.WriteLine("Ошибка ввода даты.\nПовторный ввод");
            }
            while(flag);

            return date;
        }

        // Метод выбора Person для добавления новых постулпений
        private static Person ChoicePerson()
        {
            AppFunctioality.ToPrintPerson();

            int Id;
            Console.WriteLine("Выберете пользователя - введите его Id");
            while (!Int32.TryParse(Console.ReadLine(), out Id))
                Console.WriteLine("Введенно некрорректное значение Id");

            return AppFunctioality.FindPerson(Id);
        }
    }
}
