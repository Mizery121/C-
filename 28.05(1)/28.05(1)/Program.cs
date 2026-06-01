using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// ЗАДАНИЕ 1
namespace CarRace
{
    // Делегат для старта
    public delegate void StartRaceHandler();

    // Абстрактный класс автомобиля
    public abstract class Car
    {
        private static int _nextId = 0;
        private static readonly Random _globalRand = new Random();
        [ThreadStatic] private static Random _localRand;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Position { get; private set; }  // 0..100
        public double CurrentSpeed { get; private set; }

        // Событие финиша
        public event EventHandler<Car> Finished;

        protected Car(string name)
        {
            Id = ++_nextId;
            Name = name;
            Position = 0;
        }

        // Получить потокобезопасный random
        private Random GetRand()
        {
            if (_localRand == null)
            {
                lock (_globalRand)
                {
                    _localRand = new Random(_globalRand.Next());
                }
            }
            return _localRand;
        }

        // Абстрактный метод минимальная и максимальная скорость
        protected abstract void GetSpeedRange(out int min, out int max);

        public void Move()
        {
            if (Position >= 100) return;
            GetSpeedRange(out int min, out int max);
            CurrentSpeed = GetRand().Next(min, max + 1);
            Position += CurrentSpeed / 10.0;
            if (Position >= 100)
            {
                Position = 100;
                OnFinished();
            }
        }

        protected virtual void OnFinished()
        {
            Finished?.Invoke(this, this);
        }

        public void Reset()
        {
            Position = 0;
        }

        public override string ToString() => Name;
    }

    // Конкретные типы автомобилей
    public class SportsCar : Car
    {
        public SportsCar(string name) : base(name) { }
        protected override void GetSpeedRange(out int min, out int max) { min = 80; max = 150; }
    }

    public class PassengerCar : Car
    {
        public PassengerCar(string name) : base(name) { }
        protected override void GetSpeedRange(out int min, out int max) { min = 60; max = 120; }
    }

    public class Truck : Car
    {
        public Truck(string name) : base(name) { }
        protected override void GetSpeedRange(out int min, out int max) { min = 40; max = 90; }
    }

    public class Bus : Car
    {
        public Bus(string name) : base(name) { }
        protected override void GetSpeedRange(out int min, out int max) { min = 50; max = 100; }
    }

    // Класс игры
    public class RaceGame
    {
        private List<Car> _cars;
        private bool _isRacing;
        private Car _winner;

        public event StartRaceHandler RaceStarted;

        public RaceGame(List<Car> cars)
        {
            _cars = cars;
            _isRacing = false;
            _winner = null;
            foreach (var car in _cars)
                car.Finished += OnCarFinished;
        }

        private void OnCarFinished(object sender, Car car)
        {
            if (_isRacing && _winner == null)
            {
                _winner = car;
                _isRacing = false;
            }
        }

        public void StartRace()
        {
            _isRacing = true;
            _winner = null;
            foreach (var car in _cars) car.Reset();

            RaceStarted?.Invoke();
            Console.WriteLine("\n=== ГОНКА НАЧАЛАСЬ ===\n");

            while (_isRacing)
            {
                Console.Clear();
                Console.WriteLine("=== АВТОМОБИЛЬНЫЕ ГОНКИ ===\n");
                foreach (var car in _cars)
                {
                    car.Move();
                    int bar = (int)(car.Position / 2);
                    string progress = new string('#', bar) + new string('-', 50 - bar);
                    Console.WriteLine($"{car.Name,-15} [{progress}] {car.Position,5:F1} км");
                }
                Thread.Sleep(150);
                if (Console.KeyAvailable) break;
            }

            Console.WriteLine("\n" + new string('=', 50));
            if (_winner != null)
                Console.WriteLine($"ПОБЕДИТЕЛЬ: {_winner.Name} (дистанция {_winner.Position:F0} км)");
            else
                Console.WriteLine("Гонка прервана.");
        }
    }

    public static class RaceDemo
    {
        public static void Run()
        {
            var cars = new List<Car>
            {
                new SportsCar("Ferrari"),
                new PassengerCar("Toyota"),
                new Truck("Volvo"),
                new Bus("MAN")
            };
            var game = new RaceGame(cars);
            game.RaceStarted += () => Console.WriteLine("Старт!");
            game.StartRace();
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}

// ЗАДАНИЕ 2
namespace CardGame
{
    // Масти
    public enum Suit
    {
        Hearts,   // Черви
        Diamonds, // Бубны
        Clubs,    // Крести
        Spades    // Пики
    }

    // Класс карты
    public class Karta : IComparable<Karta>
    {
        public Suit Suit { get; private set; }
        public string Rank { get; private set; }
        public int Value { get; private set; } // 6-14

        public Karta(Suit suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public int CompareTo(Karta other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            string suitSymbol = "";
            switch (Suit)
            {
                case Suit.Hearts: suitSymbol = "Ч"; break;
                case Suit.Diamonds: suitSymbol = "Б"; break;
                case Suit.Clubs: suitSymbol = "Т"; break;
                case Suit.Spades: suitSymbol = "П"; break;
            }
            return Rank + suitSymbol;
        }
    }

    // Игрок
    public class Player
    {
        public string Name { get; private set; }
        private Queue<Karta> _cards = new Queue<Karta>();

        public Player(string name) { Name = name; }

        public void AddCards(IEnumerable<Karta> cards)
        {
            foreach (var c in cards) _cards.Enqueue(c);
        }

        public Karta PlayCard()
        {
            return _cards.Count > 0 ? _cards.Dequeue() : null;
        }

        public int CardsCount => _cards.Count;

        public bool HasCards => _cards.Count > 0;

        public void ShowCards()
        {
            Console.WriteLine($"{Name} ({CardsCount} карт): " + string.Join(", ", _cards));
        }
    }

    // Игра
    public class Game
    {
        private List<Player> _players;
        private List<Karta> _deck;
        private Random _rand = new Random();

        public Game(List<string> playerNames)
        {
            if (playerNames.Count < 2)
                throw new Exception("Нужно минимум 2 игрока");
            _players = playerNames.Select(n => new Player(n)).ToList();
            CreateDeck();
            Shuffle();
            Deal();
        }

        private void CreateDeck()
        {
            _deck = new List<Karta>();
            string[] ranks = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            int[] values = { 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                for (int i = 0; i < ranks.Length; i++)
                    _deck.Add(new Karta(suit, ranks[i], values[i]));
        }

        private void Shuffle()
        {
            for (int i = _deck.Count - 1; i > 0; i--)
            {
                int j = _rand.Next(i + 1);
                var temp = _deck[i];
                _deck[i] = _deck[j];
                _deck[j] = temp;
            }
        }

        private void Deal()
        {
            int perPlayer = _deck.Count / _players.Count;
            int idx = 0;
            foreach (var p in _players)
            {
                p.AddCards(_deck.Skip(idx).Take(perPlayer));
                idx += perPlayer;
            }
            // остатки первому
            if (idx < _deck.Count)
                _players[0].AddCards(_deck.Skip(idx));
        }

        public void Play()
        {
            Console.WriteLine("\n=== НАЧАЛО ИГРЫ ===");
            int round = 0;
            while (_players.Count(p => p.HasCards) > 1)
            {
                round++;
                Console.WriteLine($"\n--- Раунд {round} ---");
                var played = new List<Tuple<Player, Karta>>();

                foreach (var p in _players.Where(p => p.HasCards))
                {
                    var card = p.PlayCard();
                    if (card != null)
                        played.Add(Tuple.Create(p, card));
                }

                // Вывод сыгранных карт
                foreach (var t in played)
                    Console.WriteLine($"{t.Item1.Name} кладёт {t.Item2}");

                // Определение победителя раунда
                var winner = played.OrderByDescending(t => t.Item2.Value).First().Item1;
                Console.WriteLine($"Забирает {winner.Name}");

                // Победитель забирает все карты раунда
                winner.AddCards(played.Select(t => t.Item2));

                foreach (var p in _players)
                    Console.WriteLine($"{p.Name}: {p.CardsCount} карт");

                Thread.Sleep(500);
            }

            var finalWinner = _players.FirstOrDefault(p => p.HasCards);
            Console.WriteLine($"\n*** ПОБЕДИТЕЛЬ: {finalWinner?.Name} ***");
        }
    }

    public static class CardDemo
    {
        public static void Run()
        {
            Console.WriteLine("Карточная игра.");
            Console.Write("Введите имена игроков через запятую: ");
            string input = Console.ReadLine();
            var names = input.Split(',').Select(s => s.Trim()).ToList();
            if (names.Count < 2)
                names = new List<string> { "Алиса", "Боб" };

            var game = new Game(names);
            game.Play();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}

// ГЛАВНАЯ ПРОГРАММА
namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:");
                Console.WriteLine("1. Автомобильные гонки");
                Console.WriteLine("2. Карточная игра");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                if (choice == "1") CarRace.RaceDemo.Run();
                else if (choice == "2") CardGame.CardDemo.Run();
                else if (choice == "0") break;
                else Console.WriteLine("Неверный ввод. Нажмите Enter...");
                Console.ReadLine();
            }
        }
    }
}