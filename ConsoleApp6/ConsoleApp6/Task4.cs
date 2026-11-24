using System.Diagnostics;
using System.Text;

static class Task4
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

        var sw = Stopwatch.StartNew();
        string text = File.ReadAllText(path, Encoding.UTF8);

        char[] chars = text.ToCharArray();
        Array.Reverse(chars);

        string dir = Path.GetDirectoryName(Path.GetFullPath(path)) ?? ".";
        string outPath = Path.Combine(dir,
            Path.GetFileNameWithoutExtension(path) + "_reversed" + Path.GetExtension(path));
        File.WriteAllText(outPath, new string(chars), Encoding.UTF8);
        sw.Stop();

        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Вхідний файл:            {path}");
        Console.WriteLine($"Символів оброблено:      {chars.Length}");
        Console.WriteLine($"Новий файл:              {outPath}");
        Console.WriteLine($"Час роботи:              {sw.ElapsedMilliseconds} мс");
    }
}