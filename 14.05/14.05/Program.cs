using System;

namespace ArrayTasksNoMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задание 1: Сжать массив, удалив все 0, заполнить освободившиеся справа элементы -1
            Console.WriteLine("=== Задание 1: Сжатие массива (удалить нули, заполнить -1) ===");
            int[] arr1 = ReadIntArray("Введите элементы массива (через пробел): ");
            Console.WriteLine("Исходный массив: " + string.Join(" ", arr1));
            int[] compressed = CompressArray(arr1);
            Console.WriteLine("Результат: " + string.Join(" ", compressed));

            // Задание 2: Преобразовать массив: сначала все отрицательные, потом положительные (0 считать положительным)
            Console.WriteLine("\n=== Задание 2: Отрицательные, затем положительные (0 — положительный) ===");
            int[] arr2 = ReadIntArray("Введите элементы массива (через пробел): ");
            Console.WriteLine("Исходный массив: " + string.Join(" ", arr2));
            int[] transformed = NegativesThenPositives(arr2);
            Console.WriteLine("Преобразованный массив: " + string.Join(" ", transformed));

            // Задание 3: Подсчитать, сколько раз число встречается в массиве
            Console.WriteLine("\n=== Задание 3: Подсчёт вхождений числа в массив ===");
            int[] arr3 = ReadIntArray("Введите элементы массива (через пробел): ");
            Console.Write("Введите число для поиска: ");
            int target = ReadInt();
            int count = CountOccurrences(arr3, target);
            Console.WriteLine($"Число {target} встречается в массиве {count} раз(а).");

            // Задание 4: В двумерном массиве поменять местами заданные столбцы
            Console.WriteLine("\n=== Задание 4: Обмен столбцов в двумерном массиве ===");
            int M = ReadPositiveInt("Введите количество строк M: ");
            int N = ReadPositiveInt("Введите количество столбцов N: ");
            int[,] matrix = Read2DArray(M, N);
            Console.WriteLine("Исходная матрица:");
            Print2DArray(matrix);
            Console.Write("Введите номер первого столбца для обмена (1..N): ");
            int col1 = ReadIntInRange(1, N);
            Console.Write("Введите номер второго столбца для обмена (1..N): ");
            int col2 = ReadIntInRange(1, N);
            SwapColumns(matrix, col1 - 1, col2 - 1);
            Console.WriteLine("Матрица после обмена столбцов:");
            Print2DArray(matrix);

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // ---- Реализация заданий ----

        // Задание 1
        static int[] CompressArray(int[] arr)
        {
            int[] result = new int[arr.Length];
            int writeIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                    result[writeIndex++] = arr[i];
            }
            for (int i = writeIndex; i < result.Length; i++)
                result[i] = -1;
            return result;
        }

        // Задание 2
        static int[] NegativesThenPositives(int[] arr)
        {
            int[] result = new int[arr.Length];
            int negCount = 0;
            foreach (int val in arr)
                if (val < 0) negCount++;

            int negIdx = 0, posIdx = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                    result[negIdx++] = arr[i];
                else
                    result[negCount + posIdx++] = arr[i];
            }
            return result;
        }

        // Задание 3
        static int CountOccurrences(int[] arr, int target)
        {
            int count = 0;
            foreach (int val in arr)
                if (val == target) count++;
            return count;
        }

        // Задание 4
        static void SwapColumns(int[,] matrix, int colA, int colB)
        {
            int rows = matrix.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                int temp = matrix[i, colA];
                matrix[i, colA] = matrix[i, colB];
                matrix[i, colB] = temp;
            }
        }

        // ---- Вспомогательные методы ввода/вывода ----

        static int[] ReadIntArray(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] result = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out result[i]))
                {
                    Console.WriteLine($"Некорректный ввод '{parts[i]}', будет заменён на 0.");
                    result[i] = 0;
                }
            }
            return result;
        }

        static int ReadInt()
        {
            int val;
            while (!int.TryParse(Console.ReadLine(), out val))
                Console.Write("Ошибка, введите целое число: ");
            return val;
        }

        static int ReadPositiveInt(string prompt)
        {
            Console.Write(prompt);
            int val;
            while (true)
            {
                val = ReadInt();
                if (val > 0) break;
                Console.Write("Число должно быть положительным. Повторите: ");
            }
            return val;
        }

        static int ReadIntInRange(int min, int max)
        {
            int val;
            while (true)
            {
                val = ReadInt();
                if (val >= min && val <= max) break;
                Console.Write($"Число должно быть от {min} до {max}. Повторите: ");
            }
            return val;
        }

        static int[,] Read2DArray(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            Console.WriteLine($"Введите элементы матрицы {rows}x{cols} построчно (через пробел):");
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"Строка {i + 1}: ");
                string[] parts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < cols; j++)
                {
                    if (j < parts.Length && int.TryParse(parts[j], out int val))
                        matrix[i, j] = val;
                    else
                        matrix[i, j] = 0;
                }
            }
            return matrix;
        }

        static void Print2DArray(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
}