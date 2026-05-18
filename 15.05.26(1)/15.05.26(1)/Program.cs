using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Квадрат из символа
            Console.WriteLine("=== 1. Квадрат ===");
            DrawSquare(5, '#');

            // 2. Проверка палиндрома
            Console.WriteLine("\n=== 2. Палиндром ===");
            Console.WriteLine($"1221 -> {IsPalindrome(1221)}");
            Console.WriteLine($"3443 -> {IsPalindrome(3443)}");
            Console.WriteLine($"7854 -> {IsPalindrome(7854)}");

            // 3. Фильтрация массива
            Console.WriteLine("\n=== 3. Фильтрация ===");
            int[] original = { 1, 2, 6, -1, 88, 7, 6 };
            int[] filter = { 6, 88, 7 };
            int[] filtered = FilterArray(original, filter);
            Console.WriteLine("Оригинал:   " + string.Join(" ", original));
            Console.WriteLine("Фильтр:     " + string.Join(" ", filter));
            Console.WriteLine("Результат:  " + string.Join(" ", filtered));

            // 4. Веб-сайт
            Console.WriteLine("\n=== 4. Веб-сайт ===");
            Website website = new Website();
            website.InputData();
            website.DisplayData();

            // 5. Журнал
            Console.WriteLine("\n=== 5. Журнал ===");
            Magazine magazine = new Magazine();
            magazine.InputData();
            magazine.DisplayData();

            // 6. Магазин
            Console.WriteLine("\n=== 6. Магазин ===");
            Shop shop = new Shop();
            shop.InputData();
            shop.DisplayData();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Метод для рисования квадрата
        static void DrawSquare(int side, char symbol)
        {
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                    Console.Write(symbol);
                Console.WriteLine();
            }
        }

        // Метод проверки числа на палиндром
        static bool IsPalindrome(int number)
        {
            string str = Math.Abs(number).ToString();
            int left = 0, right = str.Length - 1;
            while (left < right)
                if (str[left++] != str[right--])
                    return false;
            return true;
        }

        // Метод фильтрации массива
        static int[] FilterArray(int[] original, int[] filter)
        {
            HashSet<int> filterSet = new HashSet<int>(filter);
            return original.Where(x => !filterSet.Contains(x)).ToArray();
        }
    }

    // Класс Веб-сайт
    class Website
    {
        private string name;
        private string path;
        private string description;
        private string ipAddress;

        public void InputData()
        {
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
            Console.WriteLine($"\nИнформация о сайте:\nНазвание: {name}\nПуть: {path}\nОписание: {description}\nIP: {ipAddress}");
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
            Console.Write("Название журнала: ");
            title = Console.ReadLine();
            Console.Write("Год основания: ");
            foundationYear = int.Parse(Console.ReadLine());
            Console.Write("Описание журнала: ");
            description = Console.ReadLine();
            Console.Write("Контактный телефон: ");
            phone = Console.ReadLine();
            Console.Write("Контактный e-mail: ");
            email = Console.ReadLine();
        }

        public void DisplayData()
        {
            Console.WriteLine($"\nИнформация о журнале:\nНазвание: {title}\nГод основания: {foundationYear}\nОписание: {description}\nТелефон: {phone}\nE-mail: {email}");
        }

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
            Console.Write("Название магазина: ");
            name = Console.ReadLine();
            Console.Write("Адрес магазина: ");
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
            Console.WriteLine($"\nИнформация о магазине:\nНазвание: {name}\nАдрес: {address}\nПрофиль: {profileDescription}\nТелефон: {phone}\nE-mail: {email}");
        }

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