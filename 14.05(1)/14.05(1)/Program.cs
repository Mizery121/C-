using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTaskApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Чётные, нечётные, уникальные элементы массива
            Console.WriteLine("=== Задание 1: Чётные, нечётные, уникальные элементы ===");
            int[] array1 = ReadIntArray("Введите элементы одномерного массива (через пробел): ");
            AnalyzeArray(array1);

            // 2. Количество значений меньше заданного параметра
            Console.WriteLine("\n=== Задание 2: Количество элементов меньше заданного числа ===");
            Console.Write("Введите число для сравнения: ");
            int threshold = ReadInt();
            int lessCount = CountLessThan(array1, threshold);
            Console.WriteLine($"Количество элементов меньше {threshold}: {lessCount}");

            // 3. Подсчёт вхождений последовательности из трёх чисел
            Console.WriteLine("\n=== Задание 3: Поиск последовательности из трёх чисел ===");
            int[] sequence = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Введите число {i + 1} последовательности: ");
                sequence[i] = ReadInt();
            }
            int seqCount = CountSequenceOccurrences(array1, sequence);
            Console.WriteLine($"Последовательность {{{string.Join(", ", sequence)}}} встречается {seqCount} раз(а).");

            // 4. Общие элементы двух массивов без повторений
            Console.WriteLine("\n=== Задание 4: Общие элементы двух массивов без повторений ===");
            int[] arrayA = ReadIntArray("Введите элементы первого массива (через пробел): ");
            int[] arrayB = ReadIntArray("Введите элементы второго массива (через пробел): ");
            int[] common = GetCommonDistinct(arrayA, arrayB);
            Console.WriteLine("Общие элементы без повторений: " + (common.Length > 0 ? string.Join(", ", common) : "нет"));

            // 5. Минимум и максимум в двумерном массиве
            Console.WriteLine("\n=== Задание 5: Минимум и максимум в двумерном массиве ===");
            int rows = ReadPositiveInt("Введите количество строк: ");
            int cols = ReadPositiveInt("Введите количество столбцов: ");
            int[,] matrix = Read2DArray(rows, cols);
            (int min, int max) = FindMinMax(matrix);
            Console.WriteLine($"Минимальное значение: {min}");
            Console.WriteLine($"Максимальное значение: {max}");

            // 6. Количество слов в предложении
            Console.WriteLine("\n=== Задание 6: Количество слов в предложении ===");
            Console.Write("Введите предложение: ");
            string sentence = Console.ReadLine();
            int wordCount = CountWords(sentence);
            Console.WriteLine($"Количество слов: {wordCount}");

            // 7. Переворот каждого слова в предложении
            Console.WriteLine("\n=== Задание 7: Переворот каждого слова ===");
            string reversedSentence = ReverseWords(sentence);
            Console.WriteLine($"Результат: {reversedSentence}");

            // 8. Количество гласных букв в предложении
            Console.WriteLine("\n=== Задание 8: Количество гласных букв ===");
            int vowelCount = CountVowels(sentence);
            Console.WriteLine($"Количество гласных букв: {vowelCount}");

            // 9. Количество вхождений подстроки в строку
            Console.WriteLine("\n=== Задание 9: Поиск подстроки в строке ===");
            Console.Write("Введите исходную строку: ");
            string text = Console.ReadLine();
            Console.Write("Введите подстроку для поиска: ");
            string substring = Console.ReadLine();
            int occurrences = CountSubstringOccurrences(text, substring);
            Console.WriteLine($"Подстрока \"{substring}\" встречается {occurrences} раз(а).");

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // ---------- Реализация заданий ----------

        // Задание 1
        static void AnalyzeArray(int[] arr)
        {
            int even = 0, odd = 0;
            HashSet<int> unique = new HashSet<int>();
            foreach (int num in arr)
            {
                if (num % 2 == 0) even++;
                else odd++;
                unique.Add(num);
            }
            Console.WriteLine($"Чётных: {even}");
            Console.WriteLine($"Нечётных: {odd}");
            Console.WriteLine($"Уникальных: {unique.Count}");
        }

        // Задание 2
        static int CountLessThan(int[] arr, int threshold)
        {
            int count = 0;
            foreach (int num in arr)
                if (num < threshold) count++;
            return count;
        }

        // Задание 3
        static int CountSequenceOccurrences(int[] arr, int[] seq)
        {
            if (seq.Length == 0 || arr.Length < seq.Length) return 0;
            int count = 0;
            for (int i = 0; i <= arr.Length - seq.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < seq.Length; j++)
                {
                    if (arr[i + j] != seq[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) count++;
            }
            return count;
        }

        // Задание 4
        static int[] GetCommonDistinct(int[] arr1, int[] arr2)
        {
            HashSet<int> set1 = new HashSet<int>(arr1);
            HashSet<int> common = new HashSet<int>();
            foreach (int num in arr2)
                if (set1.Contains(num))
                    common.Add(num);
            return common.ToArray();
        }

        // Задание 5
        static (int min, int max) FindMinMax(int[,] matrix)
        {
            int min = matrix[0, 0];
            int max = matrix[0, 0];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < min) min = matrix[i, j];
                    if (matrix[i, j] > max) max = matrix[i, j];
                }
            return (min, max);
        }

        // Задание 6
        static int CountWords(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence)) return 0;
            string[] words = sentence.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        // Задание 7
        static string ReverseWords(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence)) return "";
            string[] words = sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                char[] chars = words[i].ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }
            return string.Join(" ", words);
        }

        // Задание 8
        static int CountVowels(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return 0;
            HashSet<char> vowels = new HashSet<char>("aeiouAEIOUаеёиоуыэюяАЕЁИОУЫЭЮЯ");
            int count = 0;
            foreach (char c in sentence)
                if (vowels.Contains(c)) count++;
            return count;
        }

        // Задание 9
        static int CountSubstringOccurrences(string text, string substring)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(substring)) return 0;
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(substring, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index += substring.Length;
            }
            return count;
        }

        // ---------- Вспомогательные методы ----------
        static int[] ReadIntArray(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] result = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                if (!int.TryParse(parts[i], out result[i]))
                    result[i] = 0;
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
    }
}