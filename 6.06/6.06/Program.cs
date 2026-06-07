using System;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Invoice
{
    // Обязательные поля
    public double PaymentPerDay { get; set; }
    public int DaysCount { get; set; }
    public double PenaltyPerDay { get; set; }
    public int DelayDays { get; set; }

    // Вычисляемые поля (будут сохраняться/читаться)
    public double AmountWithoutPenalty { get; set; }
    public double Penalty { get; set; }
    public double TotalAmount { get; set; }

    // Статическое свойство для управления сериализацией
    public static bool SerializeCalculatedFields { get; set; } = true;

    // Конструктор по умолчанию (нужен для XmlSerializer)
    public Invoice() { }

    public Invoice(double paymentPerDay, int daysCount, double penaltyPerDay, int delayDays)
    {
        PaymentPerDay = paymentPerDay;
        DaysCount = daysCount;
        PenaltyPerDay = penaltyPerDay;
        DelayDays = delayDays;
        Recalculate();
    }

    public void Recalculate()
    {
        AmountWithoutPenalty = PaymentPerDay * DaysCount;
        Penalty = PenaltyPerDay * DelayDays;
        TotalAmount = AmountWithoutPenalty + Penalty;
    }

    // Этот метод управляет сериализацией: если флаг false – вычисляемые поля не сохраняем
    public bool ShouldSerializeAmountWithoutPenalty() => SerializeCalculatedFields;
    public bool ShouldSerializePenalty() => SerializeCalculatedFields;
    public bool ShouldSerializeTotalAmount() => SerializeCalculatedFields;

    public override string ToString()
    {
        return $"Оплата за день: {PaymentPerDay:C}\n" +
               $"Количество дней: {DaysCount}\n" +
               $"Сумма без штрафа: {AmountWithoutPenalty:C}\n" +
               $"Штраф за день: {PenaltyPerDay:C}\n" +
               $"Дней задержки: {DelayDays}\n" +
               $"Штраф: {Penalty:C}\n" +
               $"ИТОГО: {TotalAmount:C}\n";
    }
}

class Program
{
    static void Main()
    {
        Invoice inv = new Invoice(500, 30, 50, 5);

        Console.WriteLine("=== Исходный объект ===");
        Console.WriteLine(inv);

        // 1. Сериализация с вычисляемыми полями
        Invoice.SerializeCalculatedFields = true;
        SerializeToXml(inv, "invoice_true.xml");
        Invoice restoredTrue = DeserializeFromXml("invoice_true.xml");
        Console.WriteLine("=== Восстановленный объект (с вычисляемыми полями) ===");
        Console.WriteLine(restoredTrue);

        // 2. Сериализация без вычисляемых полей
        Invoice.SerializeCalculatedFields = false;
        SerializeToXml(inv, "invoice_false.xml");
        Invoice restoredFalse = DeserializeFromXml("invoice_false.xml");

        Console.WriteLine("=== Восстановленный объект (БЕЗ вычисляемых полей) ===");
        Console.WriteLine(restoredFalse);
        Console.WriteLine("(вычисляемые поля = 0, так как не были сохранены)");

        restoredFalse.Recalculate();
        Console.WriteLine("=== После пересчёта ===");
        Console.WriteLine(restoredFalse);

        Console.ReadKey();
    }

    static void SerializeToXml(Invoice obj, string file)
    {
        XmlSerializer xml = new XmlSerializer(typeof(Invoice));
        using (FileStream fs = new FileStream(file, FileMode.Create))
            xml.Serialize(fs, obj);
        Console.WriteLine($"XML сохранён в {file}\n");
    }

    static Invoice DeserializeFromXml(string file)
    {
        XmlSerializer xml = new XmlSerializer(typeof(Invoice));
        using (FileStream fs = new FileStream(file, FileMode.Open))
            return (Invoice)xml.Deserialize(fs);
    }
}