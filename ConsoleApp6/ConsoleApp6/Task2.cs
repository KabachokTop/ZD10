using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

static class Task2
{
    public static void Run()
    {
        Console.Write("Шлях до файлу: ");
        string path = (Console.ReadLine() ?? "").Trim().Trim('"');
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено.");
            return;
        }

        Console.Write("Слово для пошуку: ");
        string search = Console.ReadLine() ?? "";
        Console.Write("Слово для заміни: ");
        string replacement = Console.ReadLine() ?? "";

        if (search.Length == 0)
        {
            Console.WriteLine("Слово для пошуку не може бути порожнім.");
            return;
        }

        var sw = Stopwatch.StartNew();
        string text = File.ReadAllText(path, Encoding.UTF8);

        var regex = new Regex(@"(?<!\w)" + Regex.Escape(search) + @"(?!\w)",
                              RegexOptions.IgnoreCase);
        int count = regex.Matches(text).Count;

        string result = regex.Replace(text, replacement.Replace("$", "$$"));
        File.WriteAllText(path, result, Encoding.UTF8);
        sw.Stop();

        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Файл:                    {path}");
        Console.WriteLine($"Шукане слово:            {search}");
        Console.WriteLine($"Слово для заміни:        {replacement}");
        Console.WriteLine($"Кількість замін:         {count}");
        Console.WriteLine($"Розмір до / після:       {text.Length} / {result.Length} символів");
        Console.WriteLine($"Час роботи:              {sw.ElapsedMilliseconds} мс");
    }
}