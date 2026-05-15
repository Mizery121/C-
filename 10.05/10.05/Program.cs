using System;
using System.Globalization;

namespace ConsoleTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задание 1: FizzBuzz
            Console.WriteLine("\n=== Задание 1: FizzBuzz ===");
            Console.Write("Введите число от 1 до 100: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number < 1 || number > 100)
                    Console.WriteLine("Ошибка: число должно быть в диапазоне от 1 до 100.");
                else if (number % 3 == 0 && number % 5 == 0)
                    Console.WriteLine("Fizz Buzz");
                else if (number % 3 == 0)
                    Console.WriteLine("Fizz");
                else if (number % 5 == 0)
                    Console.WriteLine("Buzz");
                else
                    Console.WriteLine(number);
            }
            else
                Console.WriteLine("Ошибка: введите целое число.");

            // Задание 2: процент от числа
            Console.WriteLine("\n=== Задание 2: Вычисление процента ===");
            Console.Write("Введите число: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                Console.Write("Введите процент: ");
                if (double.TryParse(Console.ReadLine(), out double percent))
                {
                    double result = value * percent / 100;
                    Console.WriteLine($"{percent}% от {value} = {result}");
                }
                else Console.WriteLine("Ошибка: некорректный процент.");
            }
            else Console.WriteLine("Ошибка: некорректное число.");

            // Задание 3: формирование числа из четырёх цифр
            Console.WriteLine("\n=== Задание 3: Число из четырёх цифр ===");
            int[] digits = new int[4];
            bool validInput = true;
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Введите цифру {i + 1}: ");
                string input = Console.ReadLine();
                if (input.Length == 1 && char.IsDigit(input[0]))
                    digits[i] = input[0] - '0';
                else
                {
                    Console.WriteLine("Ошибка: нужно ввести одну цифру (0-9).");
                    validInput = false;
                    break;
                }
            }
            if (validInput)
            {
                int formedNumber = digits[0] * 1000 + digits[1] * 100 + digits[2] * 10 + digits[3];
                Console.WriteLine($"Сформированное число: {formedNumber}");
            }

            // Задание 4: обмен разрядов шестизначного числа
            Console.WriteLine("\n=== Задание 4: Обмен разрядов шестизначного числа ===");
            Console.Write("Введите шестизначное число: ");
            string numberStr = Console.ReadLine();
            if (numberStr.Length == 6 && long.TryParse(numberStr, out _))
            {
                Console.Write("Введите номер первого разряда (1-6): ");
                if (int.TryParse(Console.ReadLine(), out int pos1) && pos1 >= 1 && pos1 <= 6)
                {
                    Console.Write("Введите номер второго разряда (1-6): ");
                    if (int.TryParse(Console.ReadLine(), out int pos2) && pos2 >= 1 && pos2 <= 6)
                    {
                        char[] chars = numberStr.ToCharArray();
                        char temp = chars[pos1 - 1];
                        chars[pos1 - 1] = chars[pos2 - 1];
                        chars[pos2 - 1] = temp;
                        string result = new string(chars);
                        Console.WriteLine($"Результат обмена: {result}");
                    }
                    else Console.WriteLine("Ошибка: позиция должна быть от 1 до 6.");
                }
                else Console.WriteLine("Ошибка: позиция должна быть от 1 до 6.");
            }
            else Console.WriteLine("Ошибка: число должно быть шестизначным (100000-999999).");

            // Задание 5: сезон и день недели
            Console.WriteLine("\n=== Задание 5: Сезон и день недели по дате ===");
            Console.Write("Введите дату в формате дд.мм.гггг (например, 22.12.2021): ");
            string dateInput = Console.ReadLine();
            if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                string season = GetSeason(date);
                string dayOfWeek = date.ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                Console.WriteLine($"{season} {dayOfWeek}");
            }
            else Console.WriteLine("Ошибка: неверный формат даты. Используйте дд.мм.гггг.");

            // Задание 6: конвертация температуры
            Console.WriteLine("\n=== Задание 6: Конвертация температуры ===");
            Console.WriteLine("Выберите направление конвертации:");
            Console.WriteLine("1. Фаренгейт -> Цельсий");
            Console.WriteLine("2. Цельсий -> Фаренгейт");
            Console.Write("Ваш выбор: ");
            string convChoice = Console.ReadLine();
            if (convChoice == "1")
            {
                Console.Write("Введите температуру в градусах Фаренгейта: ");
                if (double.TryParse(Console.ReadLine(), out double f))
                {
                    double c = (f - 32) * 5 / 9;
                    Console.WriteLine($"{f} °F = {c:F2} °C");
                }
                else Console.WriteLine("Ошибка ввода числа.");
            }
            else if (convChoice == "2")
            {
                Console.Write("Введите температуру в градусах Цельсия: ");
                if (double.TryParse(Console.ReadLine(), out double c))
                {
                    double f = c * 9 / 5 + 32;
                    Console.WriteLine($"{c} °C = {f:F2} °F");
                }
                else Console.WriteLine("Ошибка ввода числа.");
            }
            else Console.WriteLine("Неверный выбор направления.");

            // Задание 7: четные числа в диапазоне с нормализацией
            Console.WriteLine("\n=== Задание 7: Чётные числа в диапазоне ===");
            Console.Write("Введите первое число (начало диапазона): ");
            if (int.TryParse(Console.ReadLine(), out int a))
            {
                Console.Write("Введите второе число (конец диапазона): ");
                if (int.TryParse(Console.ReadLine(), out int b))
                {
                    int start = Math.Min(a, b);
                    int end = Math.Max(a, b);
                    Console.WriteLine($"Чётные числа в диапазоне [{start}, {end}]:");
                    bool found = false;
                    for (int i = start; i <= end; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Console.Write(i + " ");
                            found = true;
                        }
                    }
                    if (!found) Console.WriteLine("Нет чётных чисел в данном диапазоне.");
                    else Console.WriteLine();
                }
                else Console.WriteLine("Ошибка ввода второго числа.");
            }
            else Console.WriteLine("Ошибка ввода первого числа.");

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static string GetSeason(DateTime date)
        {
            int month = date.Month;
            if (month == 12 || month == 1 || month == 2)
                return "Winter";
            if (month >= 3 && month <= 5)
                return "Spring";
            if (month >= 6 && month <= 8)
                return "Summer";
            return "Autumn";
        }
    }
}