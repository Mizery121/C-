using System;
using System.Collections.Generic;

namespace InheritanceTasks
{
    // ========================== Задание 1 ==========================
    // Базовый класс Human
    public class Human
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Human(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Человек: {FirstName} {LastName}, {Age} лет");
        }
    }

    // Строитель
    public class Builder : Human
    {
        public int ExperienceYears { get; set; }
        public string Specialization { get; set; }

        public Builder(string firstName, string lastName, int age, int experienceYears, string specialization)
            : base(firstName, lastName, age)
        {
            ExperienceYears = experienceYears;
            Specialization = specialization;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Строитель: {FirstName} {LastName}, {Age} лет, стаж {ExperienceYears} лет, спец.: {Specialization}");
        }

        public void Build()
        {
            Console.WriteLine($"{FirstName} {LastName} строит дом.");
        }
    }

    // Моряк
    public class Sailor : Human
    {
        public string Rank { get; set; }
        public int YearsAtSea { get; set; }

        public Sailor(string firstName, string lastName, int age, string rank, int yearsAtSea)
            : base(firstName, lastName, age)
        {
            Rank = rank;
            YearsAtSea = yearsAtSea;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Моряк: {FirstName} {LastName}, {Age} лет, звание {Rank}, лет в море {YearsAtSea}");
        }

        public void Sail()
        {
            Console.WriteLine($"{FirstName} {LastName} выходит в море.");
        }
    }

    // Лётчик
    public class Pilot : Human
    {
        public string AircraftType { get; set; }
        public int FlightHours { get; set; }

        public Pilot(string firstName, string lastName, int age, string aircraftType, int flightHours)
            : base(firstName, lastName, age)
        {
            AircraftType = aircraftType;
            FlightHours = flightHours;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Лётчик: {FirstName} {LastName}, {Age} лет, тип ВС {AircraftType}, налёт {FlightHours} ч");
        }

        public void Fly()
        {
            Console.WriteLine($"{FirstName} {LastName} управляет самолётом.");
        }
    }

    // ========================== Задание 2 ==========================
    // Паспорт
    public class Passport
    {
        public string Number { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }

        public Passport(string number, string fullName, DateTime birthDate, string country)
        {
            Number = number;
            FullName = fullName;
            BirthDate = birthDate;
            Country = country;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Паспорт: {Number}, {FullName}, {BirthDate:dd.MM.yyyy}, {Country}");
        }
    }

    // Загранпаспорт
    public class ForeignPassport : Passport
    {
        public string ForeignNumber { get; set; }
        public List<string> Visas { get; set; }

        public ForeignPassport(string number, string fullName, DateTime birthDate, string country,
                               string foreignNumber, List<string> visas)
            : base(number, fullName, birthDate, country)
        {
            ForeignNumber = foreignNumber;
            Visas = visas ?? new List<string>();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Загранномер: {ForeignNumber}");
            Console.WriteLine("Виза: " + (Visas.Count > 0 ? string.Join(", ", Visas) : "нет"));
        }

        public void AddVisa(string visa)
        {
            Visas.Add(visa);
            Console.WriteLine($"Добавлена виза {visa}");
        }
    }

    // ========================== Задание 3 ==========================
    // Базовый класс Животное
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract void MakeSound();
        public virtual void Info()
        {
            Console.WriteLine($"{Name}, возраст {Age} лет");
        }
    }

    // Тигр
    public class Tiger : Animal
    {
        public string StripeColor { get; set; }

        public Tiger(string name, int age, string stripeColor) : base(name, age)
        {
            StripeColor = stripeColor;
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} рычит: Р-р-р!");
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"Тигр с полосами {StripeColor} цвета");
        }
    }

    // Крокодил
    public class Crocodile : Animal
    {
        public double LengthMeters { get; set; }

        public Crocodile(string name, int age, double lengthMeters) : base(name, age)
        {
            LengthMeters = lengthMeters;
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} шипит и щёлкает челюстями!");
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"Крокодил длиной {LengthMeters} м");
        }
    }

    // Кенгуру
    public class Kangaroo : Animal
    {
        public double JumpHeight { get; set; }

        public Kangaroo(string name, int age, double jumpHeight) : base(name, age)
        {
            JumpHeight = jumpHeight;
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} издаёт звук: тук-тук!");
        }

        public override void Info()
        {
            base.Info();
            Console.WriteLine($"Кенгуру прыгает на высоту {JumpHeight} м");
        }
    }

    // ========================== Задание 4 ==========================
    // Абстрактный класс Фигура
    public abstract class Figure
    {
        public abstract double Area();
    }

    // Прямоугольник
    public class Rectangle : Figure
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double Area() => Width * Height;
    }

    // Круг
    public class Circle : Figure
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Area() => Math.PI * Radius * Radius;
    }

    // Прямоугольный треугольник
    public class RightTriangle : Figure
    {
        public double LegA { get; set; }
        public double LegB { get; set; }

        public RightTriangle(double legA, double legB)
        {
            LegA = legA;
            LegB = legB;
        }

        public override double Area() => 0.5 * LegA * LegB;
    }

    // Трапеция
    public class Trapezoid : Figure
    {
        public double Base1 { get; set; }
        public double Base2 { get; set; }
        public double Height { get; set; }

        public Trapezoid(double base1, double base2, double height)
        {
            Base1 = base1;
            Base2 = base2;
            Height = height;
        }

        public override double Area() => (Base1 + Base2) / 2 * Height;
    }

    // ========================== Точка входа ==========================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Демонстрация задания 1
            Console.WriteLine("=== Задание 1. Human, Builder, Sailor, Pilot ===\n");
            var builder = new Builder("Иван", "Иванов", 35, 10, "Монтажник");
            var sailor = new Sailor("Пётр", "Петров", 42, "Капитан", 20);
            var pilot = new Pilot("Сергей", "Сергеев", 28, "Boeing 737", 1500);

            builder.DisplayInfo();
            builder.Build();
            sailor.DisplayInfo();
            sailor.Sail();
            pilot.DisplayInfo();
            pilot.Fly();

            // Демонстрация задания 2
            Console.WriteLine("\n=== Задание 2. Passport, ForeignPassport ===\n");
            var passport = new Passport("4510 123456", "Анна Смирнова", new DateTime(1990, 5, 15), "Россия");
            passport.DisplayInfo();

            var foreign = new ForeignPassport("4510 123456", "Анна Смирнова", new DateTime(1990, 5, 15), "Россия",
                                               "71 1234567", new List<string> { "Шенген", "США" });
            foreign.DisplayInfo();
            foreign.AddVisa("Великобритания");
            foreign.DisplayInfo();

            // Демонстрация задания 3
            Console.WriteLine("\n=== Задание 3. Животные ===\n");
            var animals = new List<Animal>
            {
                new Tiger("Шерхан", 7, "чёрные"),
                new Crocodile("Гена", 15, 5.2),
                new Kangaroo("Скип", 4, 2.5)
            };
            foreach (var animal in animals)
            {
                animal.Info();
                animal.MakeSound();
                Console.WriteLine();
            }

            // Демонстрация задания 4
            Console.WriteLine("=== Задание 4. Фигуры и площади ===\n");
            Figure[] figures = new Figure[]
            {
                new Rectangle(5, 10),
                new Circle(4),
                new RightTriangle(3, 4),
                new Trapezoid(6, 10, 5)
            };
            foreach (var fig in figures)
            {
                string type = fig.GetType().Name;
                Console.WriteLine($"Площадь {type}: {fig.Area():F2}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}