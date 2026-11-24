using System.Text;
using System.Text.RegularExpressions;

static class Task3
{
    public static void Run()
    {
        Console.Write("Шлях до файлу з текстом: ");
        string textPath = (Console.ReadLine() ?? "").Trim().Trim('"');
        Console.Write("Шлях до файлу зі словами для модерування: ");
        string wordsPath = (Console.ReadLine() ?? "").Trim().Trim('"');

        if (!File.Exists(textPath) || !File.Exists(wordsPath))
        {
            Console.WriteLine("Один із файлів не знайдено.");
            return;
        }

        string text = File.ReadAllText(textPath, Encoding.UTF8);

        var words = Regex.Split(File.ReadAllText(wordsPath, Encoding.UTF8), @"[^\p{L}\p{N}_]+")
                         .Where(w => w.Length > 0)
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .ToList();

        int replaced = 0;
        foreach (string word in words)
        {
            var regex = new Regex(@"(?<!\w)" + Regex.Escape(word) + @"(?!\w)",
                                  RegexOptions.IgnoreCase);
            text = regex.Replace(text, m =>
            {
                replaced++;
                return new string('*', m.Length);
            });
        }

        string dir = Path.GetDirectoryName(Path.GetFullPath(textPath)) ?? ".";
        string outPath = Path.Combine(dir,
            Path.GetFileNameWithoutExtension(textPath) + "_moderated" + Path.GetExtension(textPath));
        File.WriteAllText(outPath, text, Encoding.UTF8);

        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Слів для модерування:    {words.Count}");
        Console.WriteLine($"Виконано замін:          {replaced}");
        Console.WriteLine($"Вихідний файл:           {outPath}");
        Console.WriteLine();
        Console.WriteLine("Результат:");
        Console.WriteLine(text);
    }
}