using System;
using System.Collections.Generic;
using System.Linq;

// 1-4. Структуры Article, Client, RequestItem, Request

// Перечисление типов товаров
public enum ArticleType
{
    Electronics,
    Clothing,
    Food,
    Books,
    Other
}

// Структура товара
public struct Article
{
    public int Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ArticleType Type { get; set; }  // новое поле из задания №5

    public Article(int code, string name, decimal price, ArticleType type)
    {
        Code = code;
        Name = name;
        Price = price;
        Type = type;
    }
}

// Перечисление важности клиента
public enum ClientType
{
    Regular,
    VIP,
    Platinum
}

// Структура клиента
public struct Client
{
    public int Code { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public int OrdersCount { get; set; }
    public decimal TotalOrdersSum { get; set; }
    public ClientType Type { get; set; }   // новое поле из задания №6

    public Client(int code, string fullName, string address, string phone,
                  int ordersCount, decimal totalOrdersSum, ClientType type)
    {
        Code = code;
        FullName = fullName;
        Address = address;
        Phone = phone;
        OrdersCount = ordersCount;
        TotalOrdersSum = totalOrdersSum;
        Type = type;
    }
}

// Перечисление формы оплаты
public enum PayType
{
    Cash,
    Card,
    Online
}

// Структура позиции заказа
public struct RequestItem
{
    public Article Product { get; set; }
    public int Quantity { get; set; }

    public RequestItem(Article product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

// Структура заказа
public struct Request
{
    public int OrderCode { get; set; }
    public Client Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public List<RequestItem> Items { get; set; }
    public PayType Payment { get; set; }   // новое поле из задания №7

    // Вычисляемое свойство – сумма заказа
    public decimal TotalAmount
    {
        get
        {
            if (Items == null) return 0;
            return Items.Sum(i => i.Product.Price * i.Quantity);
        }
    }

    public Request(int orderCode, Client customer, DateTime orderDate,
                   List<RequestItem> items, PayType payment)
    {
        OrderCode = orderCode;
        Customer = customer;
        OrderDate = orderDate;
        Items = items ?? new List<RequestItem>();
        Payment = payment;
    }
}

// 8. Класс Student
public class Student
{
    // Основные данные
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Group { get; set; }
    public int Age { get; set; }

    // Зубчатый массив оценок: [программирование][], [администрирование][], [дизайн][]
    private int[][] grades;

    public Student(string lastName, string firstName, string middleName,
                   string group, int age)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Group = group;
        Age = age;
        grades = new int[3][];
        grades[0] = new int[0]; // программирование
        grades[1] = new int[0]; // администрирование
        grades[2] = new int[0]; // дизайн
    }

    // Установить оценку по предмету (subject: 0-прогр, 1-админ, 2-дизайн)
    public void SetGrade(int subject, int grade)
    {
        if (subject < 0 || subject > 2) throw new ArgumentException("Неверный предмет");
        if (grade < 1 || grade > 5) throw new ArgumentException("Оценка от 1 до 5");
        var list = grades[subject].ToList();
        list.Add(grade);
        grades[subject] = list.ToArray();
    }

    // Получить все оценки по предмету
    public int[] GetGrades(int subject)
    {
        if (subject < 0 || subject > 2) throw new ArgumentException("Неверный предмет");
        return grades[subject].ToArray();
    }

    // Средний балл по предмету
    public double GetAverageGrade(int subject)
    {
        if (subject < 0 || subject > 2) throw new ArgumentException("Неверный предмет");
        if (grades[subject].Length == 0) return 0;
        return grades[subject].Average();
    }

    // Распечатка данных студента
    public void PrintInfo()
    {
        Console.WriteLine($"Студент: {LastName} {FirstName} {MiddleName}");
        Console.WriteLine($"Группа: {Group}, Возраст: {Age}");
        Console.WriteLine("Оценки:");
        Console.WriteLine($"  Программирование: {string.Join(", ", grades[0])} (ср. {GetAverageGrade(0):F2})");
        Console.WriteLine($"  Администрирование: {string.Join(", ", grades[1])} (ср. {GetAverageGrade(1):F2})");
        Console.WriteLine($"  Дизайн: {string.Join(", ", grades[2])} (ср. {GetAverageGrade(2):F2})");
    }
}

// 9. Приложение "7 чудес света" (разные классы в пространстве имён)
namespace SevenWonders
{
    public class GreatPyramid
    {
        public string Name => "Пирамида Хеопса";
        public string Location => "Гиза, Египет";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class HangingGardens
    {
        public string Name => "Висячие сады Семирамиды";
        public string Location => "Вавилон, Ирак";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class StatueOfZeus
    {
        public string Name => "Статуя Зевса в Олимпии";
        public string Location => "Олимпия, Греция";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class TempleOfArtemis
    {
        public string Name => "Храм Артемиды в Эфесе";
        public string Location => "Эфес, Турция";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class Mausoleum
    {
        public string Name => "Мавзолей в Галикарнасе";
        public string Location => "Бодрум, Турция";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class Colossus
    {
        public string Name => "Колосс Родосский";
        public string Location => "Родос, Греция";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
    public class Lighthouse
    {
        public string Name => "Александрийский маяк";
        public string Location => "Александрия, Египет";
        public void Show() => Console.WriteLine($"{Name} - {Location}");
    }
}

// 10. Сравнение населения столиц (пространства имён по странам)
namespace Russia
{
    public class Moscow
    {
        public long Population => 12_500_000; // примерно
        public string Name => "Москва";
    }
}
namespace Japan
{
    public class Tokyo
    {
        public long Population => 14_000_000;
        public string Name => "Токио";
    }
}
namespace USA
{
    public class WashingtonDC
    {
        public long Population => 700_000;
        public string Name => "Вашингтон";
    }
}

// Точка входа
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Демонстрация структур (задания 1-7)
        Console.WriteLine("=== Структуры Article, Client, Request ===\n");
        var article = new Article(1, "Ноутбук", 1500m, ArticleType.Electronics);
        var client = new Client(101, "Иванов И.И.", "Москва", "+7(999)123-45-67", 5, 30000m, ClientType.VIP);
        var items = new List<RequestItem> { new RequestItem(article, 2) };
        var request = new Request(5001, client, DateTime.Now, items, PayType.Card);

        Console.WriteLine($"Заказ №{request.OrderCode}: сумма = {request.TotalAmount} руб., оплата {request.Payment}");
        Console.WriteLine($"Клиент: {client.FullName}, тип {client.Type}");
        Console.WriteLine($"Товар: {article.Name}, тип {article.Type}");

        // Демонстрация Student
        Console.WriteLine("\n=== Класс Student ===\n");
        var student = new Student("Петров", "Пётр", "Сергеевич", "ИС-31", 20);
        student.SetGrade(0, 5); // программирование
        student.SetGrade(0, 4);
        student.SetGrade(1, 3);
        student.SetGrade(2, 5);
        student.PrintInfo();

        // Демонстрация 7 чудес
        Console.WriteLine("\n=== 7 чудес света ===\n");
        var pyramid = new SevenWonders.GreatPyramid();
        var gardens = new SevenWonders.HangingGardens();
        pyramid.Show();
        gardens.Show();
        // остальные можно аналогично...

        // Демонстрация сравнения столиц
        Console.WriteLine("\n=== Сравнение населения столиц ===\n");
        var msk = new Russia.Moscow();
        var tok = new Japan.Tokyo();
        var wash = new USA.WashingtonDC();
        Console.WriteLine($"{msk.Name}: {msk.Population:N0} чел.");
        Console.WriteLine($"{tok.Name}: {tok.Population:N0} чел.");
        Console.WriteLine($"{wash.Name}: {wash.Population:N0} чел.");
        if (msk.Population > tok.Population && msk.Population > wash.Population)
            Console.WriteLine($"Самое большое население у {msk.Name}");
        else if (tok.Population > msk.Population && tok.Population > wash.Population)
            Console.WriteLine($"Самое большое население у {tok.Name}");
        else
            Console.WriteLine($"Самое большое население у {wash.Name}");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}