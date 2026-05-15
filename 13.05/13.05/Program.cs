using System;
using System.Collections.Generic;

namespace HomeworkTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Квадрат из символа
            Console.WriteLine("=== Задание 1: Квадрат из символа ===");
            Console.Write("Введите длину стороны квадрата: ");
            int side = ReadIntPositive();
            Console.Write("Введите символ для заполнения: ");
            char symbol = Console.ReadKey().KeyChar;
            Console.WriteLine();
            DrawSquare(side, symbol);

            // 2. Палиндром
            Console.WriteLine("\n=== Задание 2: Проверка числа на палиндром ===");
            Console.Write("Введите целое число: ");
            int number = ReadInt();
            bool isPalindrome = IsPalindrome(number);
            Console.WriteLine($"Число {number} {(isPalindrome ? "является" : "не является")} палиндромом.");

            // 3. Фильтрация массива
            Console.WriteLine("\n=== Задание 3: Фильтрация массива ===");
            int[] originalArray = ReadIntArray("Введите оригинальный массив (через пробел): ");
            int[] filterArray = ReadIntArray("Введите массив для фильтрации (через пробел): ");
            int[] filteredResult = FilterArray(originalArray, filterArray);
            Console.WriteLine("Результат фильтрации: " + string.Join(" ", filteredResult));

            // 4. Класс Веб-сайт
            Console.WriteLine("\n=== Задание 4: Класс Веб-сайт ===");
            Website site = new Website();
            site.InputData();
            site.DisplayData();

            // 5. Класс Журнал
            Console.WriteLine("\n=== Задание 5: Класс Журнал ===");
            Magazine magazine = new Magazine();
            magazine.InputData();
            magazine.DisplayData();

            // 6. Класс Магазин
            Console.WriteLine("\n=== Задание 6: Класс Магазин ===");
            Shop shop = new Shop();
            shop.InputData();
            shop.DisplayData();

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // ---------- Методы заданий ----------

        // 1. Квадрат из символа
        static void DrawSquare(int side, char symbol)
        {
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                    Console.Write(symbol);
                Console.WriteLine();
            }
        }

        // 2. Проверка палиндрома (число)
        static bool IsPalindrome(int num)
        {
            string str = Math.Abs(num).ToString(); // учитываем только абсолютное значение
            int left = 0, right = str.Length - 1;
            while (left < right)
            {
                if (str[left] != str[right])
                    return false;
                left++;
                right--;
            }
            return true;
        }

        // 3. Фильтрация массива
        static int[] FilterArray(int[] original, int[] filter)
        {
            HashSet<int> filterSet = new HashSet<int>(filter);
            List<int> result = new List<int>();
            foreach (int item in original)
                if (!filterSet.Contains(item))
                    result.Add(item);
            return result.ToArray();
        }

        // ---------- Вспомогательные методы ввода ----------
        static int ReadInt()
        {
            int val;
            while (!int.TryParse(Console.ReadLine(), out val))
                Console.Write("Ошибка, введите целое число: ");
            return val;
        }

        static int ReadIntPositive()
        {
            int val;
            while (true)
            {
                val = ReadInt();
                if (val > 0) break;
                Console.Write("Число должно быть положительным. Повторите: ");
            }
            return val;
        }

        static int[] ReadIntArray(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] arr = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                if (!int.TryParse(parts[i], out arr[i]))
                    arr[i] = 0;
            return arr;
        }
    }

    // ---------- Классы ----------

    // Класс Веб-сайт
    class Website
    {
        private string name;
        private string path;
        private string description;
        private string ipAddress;

        public void InputData()
        {
            Console.WriteLine("Введите данные о веб-сайте:");
            Console.Write("Название сайта: ");
            name = Console.ReadLine();
            Console.Write("Путь к сайту: ");
            path = Console.ReadLine();
            Console.Write("Описание сайта: ");
            description = Console.ReadLine();
            Console.Write("IP адрес сайта: ");
            ipAddress = Console.ReadLine();
        }

        public void DisplayData()
        {
            Console.WriteLine("\nИнформация о веб-сайте:");
            Console.WriteLine($"Название: {name}");
            Console.WriteLine($"Путь: {path}");
            Console.WriteLine($"Описание: {description}");
            Console.WriteLine($"IP адрес: {ipAddress}");
        }

        // Методы доступа к полям
        public string GetName() => name;
        public void SetName(string value) => name = value;
        public string GetPath() => path;
        public void SetPath(string value) => path = value;
        public string GetDescription() => description;
        public void SetDescription(string value) => description = value;
        public string GetIpAddress() => ipAddress;
        public void SetIpAddress(string value) => ipAddress = value;
    }

    // Класс Журнал
    class Magazine
    {
        private string title;
        private int foundationYear;
        private string description;
        private string phone;
        private string email;

        public void InputData()
        {
            Console.WriteLine("Введите данные о журнале:");
            Console.Write("Название журнала: ");
            title = Console.ReadLine();
            Console.Write("Год основания: ");
            foundationYear = ReadIntYear();
            Console.Write("Описание журнала: ");
            description = Console.ReadLine();
            Console.Write("Контактный телефон: ");
            phone = Console.ReadLine();
            Console.Write("Контактный e-mail: ");
            email = Console.ReadLine();
        }

        private int ReadIntYear()
        {
            int year;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out year) && year > 0 && year <= DateTime.Now.Year)
                    break;
                Console.Write($"Введите корректный год (от 1 до {DateTime.Now.Year}): ");
            }
            return year;
        }

        public void DisplayData()
        {
            Console.WriteLine("\nИнформация о журнале:");
            Console.WriteLine($"Название: {title}");
            Console.WriteLine($"Год основания: {foundationYear}");
            Console.WriteLine($"Описание: {description}");
            Console.WriteLine($"Телефон: {phone}");
            Console.WriteLine($"E-mail: {email}");
        }

        // Методы доступа
        public string GetTitle() => title;
        public void SetTitle(string value) => title = value;
        public int GetFoundationYear() => foundationYear;
        public void SetFoundationYear(int value) => foundationYear = value;
        public string GetDescription() => description;
        public void SetDescription(string value) => description = value;
        public string GetPhone() => phone;
        public void SetPhone(string value) => phone = value;
        public string GetEmail() => email;
        public void SetEmail(string value) => email = value;
    }

    // Класс Магазин
    class Shop
    {
        private string name;
        private string address;
        private string profileDescription;
        private string phone;
        private string email;

        public void InputData()
        {
            Console.WriteLine("Введите данные о магазине:");
            Console.Write("Название магазина: ");
            name = Console.ReadLine();
            Console.Write("Адрес: ");
            address = Console.ReadLine();
            Console.Write("Описание профиля магазина: ");
            profileDescription = Console.ReadLine();
            Console.Write("Контактный телефон: ");
            phone = Console.ReadLine();
            Console.Write("Контактный e-mail: ");
            email = Console.ReadLine();
        }

        public void DisplayData()
        {
            Console.WriteLine("\nИнформация о магазине:");
            Console.WriteLine($"Название: {name}");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Профиль: {profileDescription}");
            Console.WriteLine($"Телефон: {phone}");
            Console.WriteLine($"E-mail: {email}");
        }

        // Методы доступа
        public string GetName() => name;
        public void SetName(string value) => name = value;
        public string GetAddress() => address;
        public void SetAddress(string value) => address = value;
        public string GetProfileDescription() => profileDescription;
        public void SetProfileDescription(string value) => profileDescription = value;
        public string GetPhone() => phone;
        public void SetPhone(string value) => phone = value;
        public string GetEmail() => email;
        public void SetEmail(string value) => email = value;
    }
}