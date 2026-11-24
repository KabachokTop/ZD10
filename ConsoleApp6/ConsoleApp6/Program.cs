using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Меню ===");
    Console.WriteLine("1 - Прості числа та числа Фібоначчі");
    Console.WriteLine("2 - Пошук і заміна слова у файлі");
    Console.WriteLine("3 - Модератор");
    Console.WriteLine("4 - Перевернути вміст файлу");
    Console.WriteLine("5 - Аналіз 100000 чисел");
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1": Task1.Run(); break;
            case "2": Task2.Run(); break;
            case "3": Task3.Run(); break;
            case "4": Task4.Run(); break;
            case "5": Task5.Run(); break;
            case "0": return;
            default: Console.WriteLine("Невірний вибір."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}