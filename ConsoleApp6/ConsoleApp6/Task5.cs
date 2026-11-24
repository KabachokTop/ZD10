using System.Diagnostics;
using System.Text;

static class Task5
{
    public static void Run()
    {
        Console.Write("Шлях до файлу з числами (Enter - створити тестовий numbers.txt): ");
        string path = (Console.ReadLine() ?? "").Trim().Trim('"');

        if (path.Length == 0)
        {
            path = "numbers.txt";
            GenerateTestFile(path);
            Console.WriteLine($"Створено тестовий файл {Path.GetFullPath(path)}");
        }
        else if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено.");
            return;
        }

        var sw = Stopwatch.StartNew();

        var positive = new List<long>();
        var negative = new List<long>();
        var twoDigit = new List<long>();
        var fiveDigit = new List<long>();
        int zeros = 0, total = 0, invalid = 0;

        foreach (string token in File.ReadAllText(path, Encoding.UTF8)
                     .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))
        {
            if (!long.TryParse(token, out long n))
            {
                invalid++;
                continue;
            }

            total++;
            if (n > 0) positive.Add(n);
            else if (n < 0) negative.Add(n);
            else zeros++;

            long abs = Math.Abs(n);
            if (abs >= 10 && abs <= 99) twoDigit.Add(n);
            if (abs >= 10000 && abs <= 99999) fiveDigit.Add(n);
        }

        string dir = Path.GetDirectoryName(Path.GetFullPath(path)) ?? ".";
        Save(Path.Combine(dir, "positive.txt"), positive);
        Save(Path.Combine(dir, "negative.txt"), negative);
        Save(Path.Combine(dir, "two_digit.txt"), twoDigit);
        Save(Path.Combine(dir, "five_digit.txt"), fiveDigit);
        sw.Stop();

        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Всього чисел:            {total}");
        Console.WriteLine($"Додатних:                {positive.Count} (positive.txt)");
        Console.WriteLine($"Від'ємних:               {negative.Count} (negative.txt)");
        Console.WriteLine($"Двозначних:              {twoDigit.Count} (two_digit.txt)");
        Console.WriteLine($"П'ятизначних:            {fiveDigit.Count} (five_digit.txt)");
        Console.WriteLine($"Нулів:                   {zeros}");
        if (invalid > 0)
            Console.WriteLine($"Пропущено некоректних:   {invalid}");
        Console.WriteLine($"Файли збережено в:       {dir}");
        Console.WriteLine($"Час роботи:              {sw.ElapsedMilliseconds} мс");
    }

    static void Save(string path, List<long> numbers) =>
        File.WriteAllLines(path, numbers.Select(n => n.ToString()));

    static void GenerateTestFile(string path)
    {
        var rnd = new Random();
        var sb = new StringBuilder();
        for (int i = 0; i < 100000; i++)
        {
            int digits = rnd.Next(1, 6);
            int max = (int)Math.Pow(10, digits);
            sb.AppendLine(rnd.Next(-max + 1, max).ToString());
        }
        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }
}