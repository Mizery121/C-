using System;
using System.Linq;

namespace ExceptionTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задание 1: Преобразование строки цифр в int
            Console.WriteLine("=== Задание 1 ===");
            Console.Write("Введите набор цифр (0-9) для преобразования в int: ");
            string inputDigits = Console.ReadLine();
            try
            {
                int result = ParseStringToInt(inputDigits);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Задание 2: Преобразование двоичной строки в int
            Console.WriteLine("\n=== Задание 2 ===");
            Console.Write("Введите двоичное число (только 0 и 1): ");
            string binaryString = Console.ReadLine();
            try
            {
                int decimalResult = BinaryStringToInt(binaryString);
                Console.WriteLine($"Десятичное значение: {decimalResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Задание 3: Класс Кредитная карточка
            Console.WriteLine("\n=== Задание 3 ===");
            try
            {
                CreditCard card = new CreditCard("1234567890123456", "Иванов Иван Иванович", "123", "12/25");
                Console.WriteLine("Кредитная карта успешно создана:");
                card.DisplayInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании карты: {ex.Message}");
            }

            // Дополнительный пример с некорректными данными
            try
            {
                Console.WriteLine("\nПопытка создать карту с неверным CVC:");
                CreditCard invalidCard = new CreditCard("1234567890123456", "Петров Петр", "12", "01/26");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Задание 4: Вычисление выражения с умножением
            Console.WriteLine("\n=== Задание 4 ===");
            Console.Write("Введите математическое выражение (только целые числа и оператор *): ");
            string expression = Console.ReadLine();
            try
            {
                long product = EvaluateMultiplication(expression);
                Console.WriteLine($"Результат: {product}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // 1. Преобразование строки цифр в int с проверкой переполнения
        static int ParseStringToInt(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Строка не может быть пустой.");

            foreach (char c in input)
                if (!char.IsDigit(c))
                    throw new FormatException("Строка должна содержать только цифры (0-9).");

            // Проверка на переполнение
            try
            {
                return checked(int.Parse(input));
            }
            catch (OverflowException)
            {
                throw new OverflowException("Число выходит за пределы диапазона int (от -2,147,483,648 до 2,147,483,647).");
            }
        }

        // 2. Преобразование двоичной строки в int
        static int BinaryStringToInt(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                throw new ArgumentException("Двоичная строка не может быть пустой.");

            foreach (char c in binary)
                if (c != '0' && c != '1')
                    throw new FormatException("Строка должна содержать только символы '0' и '1'.");

            // Проверка на длину для предотвращения переполнения при преобразовании
            if (binary.Length > 31) // int может хранить до 31 бита (0..30 бит для знака)
                throw new OverflowException("Двоичное число слишком длинное для типа int (максимум 31 бит).");

            try
            {
                return checked(Convert.ToInt32(binary, 2));
            }
            catch (OverflowException)
            {
                throw new OverflowException("Двоичное число выходит за пределы диапазона int.");
            }
        }

        // 4. Вычисление произведения целых чисел, разделённых '*'
        static long EvaluateMultiplication(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                throw new ArgumentException("Выражение не может быть пустым.");

            string[] parts = expr.Split('*');
            if (parts.Length == 0)
                throw new FormatException("Некорректный формат выражения.");

            long result = 1;
            foreach (string part in parts)
            {
                if (string.IsNullOrWhiteSpace(part))
                    throw new FormatException("Пустой операнд между операторами '*'. Проверьте выражение.");

                if (!int.TryParse(part.Trim(), out int number))
                    throw new FormatException($"'{part}' не является целым числом.");

                try
                {
                    result = checked(result * number);
                }
                catch (OverflowException)
                {
                    throw new OverflowException("Результат умножения превышает допустимый диапазон (long).");
                }
            }
            return result;
        }
    }

    // 3. Класс Кредитная карточка с валидацией
    class CreditCard
    {
        private string cardNumber;
        private string ownerFullName;
        private string cvc;
        private string expiryDate;

        public string CardNumber
        {
            get => cardNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 16 || !value.All(char.IsDigit))
                    throw new ArgumentException("Номер карты должен состоять из 16 цифр.");
                cardNumber = value;
            }
        }

        public string OwnerFullName
        {
            get => ownerFullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException("ФИО владельца не может быть пустым и должно содержать минимум 5 символов.");
                ownerFullName = value;
            }
        }

        public string CVC
        {
            get => cvc;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 3 || !value.All(char.IsDigit))
                    throw new ArgumentException("CVC должен состоять из 3 цифр.");
                cvc = value;
            }
        }

        public string ExpiryDate
        {
            get => expiryDate;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !System.Text.RegularExpressions.Regex.IsMatch(value, @"^(0[1-9]|1[0-2])/\d{2}$"))
                    throw new ArgumentException("Дата завершения должна быть в формате MM/yy (например, 12/25).");
                // Дополнительно можно проверить, что дата не прошла
                string[] parts = value.Split('/');
                int month = int.Parse(parts[0]);
                int year = int.Parse(parts[1]) + 2000;
                DateTime cardDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                if (cardDate < DateTime.Now)
                    throw new ArgumentException("Срок действия карты истёк.");
                expiryDate = value;
            }
        }

        public CreditCard(string cardNumber, string ownerFullName, string cvc, string expiryDate)
        {
            CardNumber = cardNumber;
            OwnerFullName = ownerFullName;
            CVC = cvc;
            ExpiryDate = expiryDate;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Номер карты: {CardNumber}");
            Console.WriteLine($"Владелец: {OwnerFullName}");
            Console.WriteLine($"CVC: {CVC}");
            Console.WriteLine($"Действительна до: {ExpiryDate}");
        }
    }
}