using System.Diagnostics;

static class Task1
{
    public static void Run()
    {
        var sw = Stopwatch.StartNew();
        var rnd = new Random();

        int[] numbers = new int[100];
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = rnd.Next(1, 1001);

        var primes = numbers.Where(IsPrime).ToList();
        var fibs = numbers.Where(IsFibonacci).ToList();

        File.WriteAllLines("primes.txt", primes.Select(n => n.ToString()));
        File.WriteAllLines("fibonacci.txt", fibs.Select(n => n.ToString()));
        sw.Stop();

        Console.WriteLine("Згенеровані числа:");
        Console.WriteLine(string.Join(" ", numbers));
        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Згенеровано чисел:       {numbers.Length}");
        Console.WriteLine($"Простих чисел:           {primes.Count} (файл primes.txt)");
        Console.WriteLine($"Чисел Фібоначчі:         {fibs.Count} (файл fibonacci.txt)");
        Console.WriteLine($"Час роботи:              {sw.ElapsedMilliseconds} мс");
        Console.WriteLine($"Папка з файлами:         {Directory.GetCurrentDirectory()}");
    }

    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n % 2 == 0) return n == 2;
        for (int i = 3; (long)i * i <= n; i += 2)
            if (n % i == 0) return false;
        return true;
    }

    static bool IsFibonacci(int n)
    {
        if (n < 0) return false;
        long x = 5L * n * n;
        return IsSquare(x + 4) || IsSquare(x - 4);
    }

    static bool IsSquare(long x)
    {
        if (x < 0) return false;
        long r = (long)Math.Sqrt(x);
        return r * r == x || (r + 1) * (r + 1) == x;
    }
}