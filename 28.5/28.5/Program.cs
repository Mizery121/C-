using System;
using System.Collections.Generic;

// Задание 1. Геометрические фигуры

public abstract class GeometricFigure
{
    public abstract double Area();
    public abstract double Perimeter();
}

// Треугольник (по трём сторонам)
public class Triangle : GeometricFigure
{
    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }

    public Triangle(double a, double b, double c)
    {
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Невалидный треугольник");
        SideA = a; SideB = b; SideC = c;
    }

    public override double Area()
    {
        double p = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }

    public override double Perimeter() => SideA + SideB + SideC;
}

// Квадрат
public class Square : GeometricFigure
{
    public double Side { get; }
    public Square(double side) { Side = side; }
    public override double Area() => Side * Side;
    public override double Perimeter() => 4 * Side;
}

// Ромб (по стороне и углу в градусах)
public class Rhombus : GeometricFigure
{
    public double Side { get; }
    public double AngleDeg { get; }
    public Rhombus(double side, double angleDeg)
    {
        Side = side;
        AngleDeg = angleDeg;
    }
    public override double Area() => Side * Side * Math.Sin(AngleDeg * Math.PI / 180);
    public override double Perimeter() => 4 * Side;
}

// Прямоугольник
public class Rectangle : GeometricFigure
{
    public double Width { get; }
    public double Height { get; }
    public Rectangle(double w, double h) { Width = w; Height = h; }
    public override double Area() => Width * Height;
    public override double Perimeter() => 2 * (Width + Height);
}

// Параллелограмм (сторона a, сторона b, угол в градусах)
public class Parallelogram : GeometricFigure
{
    public double SideA { get; }
    public double SideB { get; }
    public double AngleDeg { get; }
    public Parallelogram(double a, double b, double angleDeg)
    {
        SideA = a; SideB = b; AngleDeg = angleDeg;
    }
    public override double Area() => SideA * SideB * Math.Sin(AngleDeg * Math.PI / 180);
    public override double Perimeter() => 2 * (SideA + SideB);
}

// Трапеция (основания a,b, высота h)
public class Trapezoid : GeometricFigure
{
    public double BaseA { get; }
    public double BaseB { get; }
    public double Height { get; }
    public Trapezoid(double a, double b, double h) { BaseA = a; BaseB = b; Height = h; }
    public override double Area() => (BaseA + BaseB) / 2 * Height;
    public override double Perimeter()
    {
        // Предполагаем равнобедренную для простоты, но можно вычислить боковые стороны
        double leg = Math.Sqrt(Height * Height + Math.Pow((BaseB - BaseA) / 2, 2));
        return BaseA + BaseB + 2 * leg;
    }
}

// Круг
public class Circle : GeometricFigure
{
    public double Radius { get; }
    public Circle(double r) { Radius = r; }
    public override double Area() => Math.PI * Radius * Radius;
    public override double Perimeter() => 2 * Math.PI * Radius;
}

// Эллипс (большая и малая полуоси)
public class Ellipse : GeometricFigure
{
    public double SemiMajor { get; }
    public double SemiMinor { get; }
    public Ellipse(double a, double b) { SemiMajor = a; SemiMinor = b; }
    public override double Area() => Math.PI * SemiMajor * SemiMinor;
    public override double Perimeter()
    {
        // Формула Рамануджана (приближённая)
        double h = Math.Pow((SemiMajor - SemiMinor) / (SemiMajor + SemiMinor), 2);
        return Math.PI * (SemiMajor + SemiMinor) * (1 + 3 * h / (10 + Math.Sqrt(4 - 3 * h)));
    }
}

// Составная фигура
public class CompositeFigure : GeometricFigure
{
    private List<GeometricFigure> figures = new List<GeometricFigure>();
    public void Add(GeometricFigure fig) => figures.Add(fig);
    public override double Area()
    {
        double total = 0;
        foreach (var fig in figures) total += fig.Area();
        return total;
    }
    public override double Perimeter()
    {
        // Периметр составной фигуры не является суммой периметров (не имеет смысла),
        // поэтому можно выбросить исключение или вернуть 0.
        throw new NotImplementedException("Периметр составной фигуры не определяется");
    }
}

// Задание 2. Иерархия товаров для дистрибьюторской компании

// Базовый класс товара
public abstract class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal PurchasePrice { get; set; }  // закупочная цена
    public decimal SalePrice { get; set; }      // цена реализации
    public int Quantity { get; set; }           // текущее количество на складе

    protected Product(int id, string name, decimal purchasePrice, decimal salePrice, int quantity)
    {
        Id = id;
        Name = name;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Quantity = quantity;
    }

    public abstract string GetCategory();
}

// Бытовая химия
public class HouseholdChemical : Product
{
    public string DangerClass { get; set; } // класс опасности
    public HouseholdChemical(int id, string name, decimal purchasePrice, decimal salePrice, int quantity, string dangerClass)
        : base(id, name, purchasePrice, salePrice, quantity)
    {
        DangerClass = dangerClass;
    }
    public override string GetCategory() => "Бытовая химия";
}

// Продукты питания
public class FoodProduct : Product
{
    public DateTime ExpiryDate { get; set; }
    public FoodProduct(int id, string name, decimal purchasePrice, decimal salePrice, int quantity, DateTime expiryDate)
        : base(id, name, purchasePrice, salePrice, quantity)
    {
        ExpiryDate = expiryDate;
    }
    public override string GetCategory() => "Продукты питания";
}

// Класс управления потоком товаров
public class ProductFlowManager
{
    private List<Product> stock = new List<Product>();

    public void Arrive(Product product, int count)
    {
        // Добавление товара на склад
        var existing = stock.Find(p => p.Id == product.Id);
        if (existing != null)
            existing.Quantity += count;
        else
            stock.Add(product);
        Console.WriteLine($"Пришло: {product.Name} x{count}");
    }

    public void Sell(Product product, int count)
    {
        var existing = stock.Find(p => p.Id == product.Id);
        if (existing == null || existing.Quantity < count)
            throw new InvalidOperationException("Недостаточно товара на складе");
        existing.Quantity -= count;
        Console.WriteLine($"Продано: {product.Name} x{count} на сумму {count * product.SalePrice:C}");
    }

    public void WriteOff(Product product, int count, string reason)
    {
        var existing = stock.Find(p => p.Id == product.Id);
        if (existing == null || existing.Quantity < count)
            throw new InvalidOperationException("Недостаточно товара для списания");
        existing.Quantity -= count;
        Console.WriteLine($"Списано: {product.Name} x{count} по причине: {reason}");
    }

    public void Transfer(Product product, int count, string destination)
    {
        var existing = stock.Find(p => p.Id == product.Id);
        if (existing == null || existing.Quantity < count)
            throw new InvalidOperationException("Недостаточно товара для передачи");
        existing.Quantity -= count;
        Console.WriteLine($"Передано: {product.Name} x{count} в {destination}");
    }

    public void ShowStock()
    {
        Console.WriteLine("\nТекущий склад:");
        foreach (var p in stock)
            Console.WriteLine($"{p.Name} (ID:{p.Id}) - {p.Quantity} шт., категория: {p.GetCategory()}");
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        // Демонстрация геометрических фигур
        var square = new Square(5);
        var circle = new Circle(3);
        var composite = new CompositeFigure();
        composite.Add(square);
        composite.Add(circle);
        Console.WriteLine($"Площадь квадрата: {square.Area()}, периметр: {square.Perimeter()}");
        Console.WriteLine($"Площадь круга: {circle.Area()}, периметр: {circle.Perimeter()}");
        Console.WriteLine($"Площадь составной фигуры: {composite.Area()}");

        // Демонстрация управления товарами
        var soap = new HouseholdChemical(1, "Мыло", 20m, 45m, 100, "Безопасно");
        var milk = new FoodProduct(2, "Молоко", 35m, 65m, 50, DateTime.Now.AddDays(7));
        var manager = new ProductFlowManager();
        manager.Arrive(soap, 100);
        manager.Arrive(milk, 50);
        manager.Sell(milk, 10);
        manager.WriteOff(soap, 5, "Брак упаковки");
        manager.Transfer(soap, 20, "Филиал на ул. Ленина");
        manager.ShowStock();
    }
}