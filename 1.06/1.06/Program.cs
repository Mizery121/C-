using System;
using System.Collections.Generic;

namespace ExceptionHandlingTasks
{
    // Задание 1
    class DivisionProgram
    {
        public static void Run()
        {
            const int arraySize = 3;
            double[] results = new double[arraySize];
            int resultCount = 0;

            Console.WriteLine("=== Безопасное деление ===\n");

            while (resultCount < arraySize)
            {
                try
                {
                    Console.Write("Введите первое целое число: ");
                    int a = int.Parse(Console.ReadLine());

                    Console.Write("Введите второе целое число: ");
                    int b = int.Parse(Console.ReadLine());

                    double quotient = (double)a / b;   // деление
                    results[resultCount] = quotient;
                    resultCount++;

                    Console.WriteLine($"Результат: {quotient}");
                    Console.WriteLine($"Сохранён в массив, позиция {resultCount}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Ошибка: деление на ноль невозможно");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Массив результатов заполнен, дальнейшие вычисления невозможны");
                    break;
                }
                finally
                {
                    Console.WriteLine("Попытка выполнения операции завершена\n");
                }

                // Спросить о продолжении, если массив ещё не заполнен
                if (resultCount < arraySize)
                {
                    Console.Write("Хотите продолжить? (да/нет): ");
                    string answer = Console.ReadLine();
                    if (answer?.ToLower() != "да" && answer?.ToLower() != "yes" && answer?.ToLower() != "д")
                        break;
                }
            }

            // Вывод всех успешных результатов
            Console.WriteLine("\nВсе результаты, хранящиеся в массиве:");
            if (resultCount == 0)
                Console.WriteLine("Нет результатов.");
            else
                for (int i = 0; i < resultCount; i++)
                    Console.WriteLine($"[{i + 1}] {results[i]}");

            Console.WriteLine("\nНажмите любую клавишу для перехода к заданию 2...");
            Console.ReadKey();
        }
    }

    // Задание 2

    // Пользовательское исключение недопустимая сумма
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() { }
        public InvalidAmountException(string message) : base(message) { }
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    }

    // Пользовательское исключение недостаточно средств
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() { }
        public InsufficientFundsException(string message) : base(message) { }
        public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }
    }

    // Класс банковского счёта
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(decimal initialBalance = 0)
        {
            // Генерация уникального номера счёта
            AccountNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Balance = 0;
            Deposit(initialBalance); // начальное пополнение
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Сумма пополнения должна быть положительной");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Сумма снятия должна быть положительной");
            if (amount > Balance)
                throw new InsufficientFundsException("Недостаточно средств на счете");
            Balance -= amount;
        }
    }

    class BankProgram
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=== Банковский счёт (пользовательские исключения) ===\n");

            BankAccount account = new BankAccount(); // начальный баланс 0
            Console.WriteLine($"Открыт счёт № {account.AccountNumber}");
            Console.WriteLine($"Текущий баланс: {account.Balance:F2}\n");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Пополнить счёт");
                Console.WriteLine("2. Снять средства");
                Console.WriteLine("3. Показать баланс");
                Console.WriteLine("4. Выйти");
                Console.Write("Ваш выбор: ");

                try
                {
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите сумму пополнения: ");
                            decimal depositAmount = decimal.Parse(Console.ReadLine());
                            account.Deposit(depositAmount);
                            Console.WriteLine($"Счёт пополнен. Новый баланс: {account.Balance:F2}");
                            break;

                        case "2":
                            Console.Write("Введите сумму снятия: ");
                            decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                            account.Withdraw(withdrawAmount);
                            Console.WriteLine($"Средства сняты. Новый баланс: {account.Balance:F2}");
                            break;

                        case "3":
                            Console.WriteLine($"Баланс счёта {account.AccountNumber}: {account.Balance:F2}");
                            break;

                        case "4":
                            exit = true;
                            Console.WriteLine("До свидания!");
                            break;

                        default:
                            Console.WriteLine("Неверный пункт меню, попробуйте снова.");
                            break;
                    }
                }
                catch (InvalidAmountException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine(); // пустая строка для разделения
                }
            }

            Console.WriteLine("\nКонец работы с банковским счётом.");
        }
    }

    // Главный класс
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задание 1
            DivisionProgram.Run();

            // Задание 2
            BankProgram.Run();

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}