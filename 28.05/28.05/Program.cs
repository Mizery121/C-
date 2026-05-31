using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Задание 1
namespace StatisticsProgram
{
    class WordStatistics
    {
        public static void Run()
        {
            // Текст из приложения 1 (стихотворение Дом который построил Джек)
            string text =
                "Вот дом, Который построил Джек. " +
                "А это пшеница, Которая в темном чулане хранится " +
                "В доме, Который построил Джек. " +
                "А это веселая птица-синица, Которая часто ворует пшеницу, " +
                "Которая в темном чулане хранится " +
                "В доме, Который построил Джек.";

            // Приведение к нижнему регистру и разбиение на слова
            string[] words = text.ToLower()
                                 .Split(new char[] { ' ', '.', ',', '-', '!', '?', ';', ':', '\n', '\r' },
                                        StringSplitOptions.RemoveEmptyEntries);

            // Подсчёт статистики с помощью Dictionary<string, int>
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (wordFrequency.ContainsKey(word))
                    wordFrequency[word]++;
                else
                    wordFrequency[word] = 1;
            }

            // Вывод результатов в виде таблицы
            Console.WriteLine("=== Статистика по тексту ===");
            Console.WriteLine("{0,-15} {1,5}", "Слово", "Частота");
            Console.WriteLine(new string('-', 25));
            foreach (var pair in wordFrequency.OrderByDescending(p => p.Value))
            {
                Console.WriteLine("{0,-15} {1,5}", pair.Key, pair.Value);
            }
            Console.WriteLine($"\nВсего уникальных слов: {wordFrequency.Count}");
        }
    }
}

// Задание 2
namespace PropertyChangeNotifier
{
    // Аргументы события – хранят имя изменённого свойства
    public class PropertyEventArgs : EventArgs
    {
        public string PropertyName { get; }
        public PropertyEventArgs(string propertyName) => PropertyName = propertyName;
    }

    // Делегат для обработчика события
    public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

    // Интерфейс, оповещающий об изменении свойства
    interface IPropertyChanged
    {
        event PropertyEventHandler PropertyChanged;
    }

    // Тестовый класс, реализующий интерфейс IPropertyChanged
    class TestClass : IPropertyChanged
    {
        private string _name;
        private int _value;

        public event PropertyEventHandler PropertyChanged;

        // Метод для вызова события
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
    }

    class PropertyChangeDemo
    {
        public static void Run()
        {
            TestClass obj = new TestClass();
            // Подписка на событие
            obj.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"Свойство '{e.PropertyName}' было изменено.");
            };

            Console.WriteLine("\n=== Демонстрация уведомлений об изменении свойств ===");
            obj.Name = "Тестовое имя";
            obj.Value = 42;
            obj.Name = "Новое имя";
            obj.Value = 100;
        }
    }
}

// Точка входа в программу
namespace MainApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Выполнение задания 1
            StatisticsProgram.WordStatistics.Run();

            // Выполнение задания 2
            PropertyChangeNotifier.PropertyChangeDemo.Run();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}