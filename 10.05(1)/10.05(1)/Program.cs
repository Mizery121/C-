using System;

namespace HomeworkTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задача 1: Квадраты на прямоугольнике
            Console.WriteLine("=== Задача 1: Квадраты на прямоугольнике ===");
            int A = ReadPositiveInt("Введите целое положительное число A (длина прямоугольника): ");
            int B = ReadPositiveInt("Введите целое положительное число B (ширина прямоугольника): ");
            int C = ReadPositiveInt("Введите целое положительное число C (сторона квадрата): ");

            if (C > A || C > B)
            {
                Console.WriteLine("Сообщение: невозможно разместить ни одного квадрата, так как сторона квадрата превышает одну из сторон прямоугольника.");
            }
            else
            {
                int countX = A / C;
                int countY = B / C;
                int totalSquares = countX * countY;
                int squareArea = C * C;
                int rectangleArea = A * B;
                int occupiedArea = totalSquares * squareArea;
                int freeArea = rectangleArea - occupiedArea;

                Console.WriteLine($"Количество квадратов: {totalSquares}");
                Console.WriteLine($"Площадь незанятой части: {freeArea}");
            }

            // Задача 2: Вклад в банке
            Console.WriteLine("\n=== Задача 2: Вклад в банке ===");
            double P;
            while (true)
            {
                Console.Write("Введите вещественное число P (0 < P < 25): ");
                if (double.TryParse(Console.ReadLine(), out P) && P > 0 && P < 25)
                    break;
                Console.WriteLine("Ошибка: P должно быть в интервале (0, 25). Попробуйте снова.");
            }

            double deposit = 10000.0;
            int months = 0;
            while (deposit <= 11000)
            {
                deposit += deposit * (P / 100.0);
                months++;
            }
            Console.WriteLine($"Количество месяцев: {months}");
            Console.WriteLine($"Итоговый размер вклада: {deposit:F2} руб.");

            // Задача 3: Вывод чисел от A до B с повторением
            Console.WriteLine("\n=== Задача 3: Числа от A до B с повторением ===");
            int start, end;
            while (true)
            {
                Console.Write("Введите целое положительное число A: ");
                start = ReadInt();
                Console.Write("Введите целое положительное число B (B > A): ");
                end = ReadInt();
                if (start > 0 && end > 0 && start < end)
                    break;
                Console.WriteLine("Ошибка: A и B должны быть положительными, и A < B. Попробуйте снова.");
            }

            for (int i = start; i <= end; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(i);
                    if (j < i - 1) Console.Write(" ");
                }
                Console.WriteLine();
            }

            // Задача 4: Переворот числа
            Console.WriteLine("\n=== Задача 4: Переворот числа ===");
            int N = ReadPositiveInt("Введите целое положительное число N: ");
            int reversed = ReverseNumber(N);
            Console.WriteLine($"Число, полученное при прочтении справа налево: {reversed}");

            Console.WriteLine("\nВсе задачи выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Вспомогательные методы
        static int ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Ошибка: введите целое число. Попробуйте снова: ");
            }
            return result;
        }

        static int ReadPositiveInt(string prompt)
        {
            Console.Write(prompt);
            int value;
            while (true)
            {
                value = ReadInt();
                if (value > 0)
                    break;
                Console.Write("Ошибка: число должно быть положительным. Попробуйте снова: ");
            }
            return value;
        }

        static int ReverseNumber(int n)
        {
            int reversed = 0;
            while (n > 0)
            {
                reversed = reversed * 10 + n % 10;
                n /= 10;
            }
            return reversed;
        }
    }
}