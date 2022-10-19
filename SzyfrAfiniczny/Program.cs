public static class SzyfrAfiniczny
{
    private const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string Encipher(string text, int a, int b)
    {
        if (!IsPrime(a)) throw new ArgumentException("a nie jest liczba pierwsza");
        string output = string.Empty;
        foreach (char c in text.ToUpper())
        {
            if(!char.IsLetter(c))
            {
                output += c;
                continue;
            }
            int shift = (a * _letters.IndexOf(c) + b) % _letters.Length;
            output += _letters[shift];
        }
        return output;
    }

    public static string Decipher(string text, int a, int b)
    {
        if (!IsPrime(a)) throw new ArgumentException("a nie jest liczba pierwsza");
        int inverse = Enumerable.Range(1, _letters.Length).First(i => i * a % _letters.Length == 1);
        string output = string.Empty;
        foreach (char c in text.ToUpper())
        {
            if(!char.IsLetter(c))
            {
                output += c;
                continue;
            }
            int index = inverse * (_letters.IndexOf(c) - b) % _letters.Length;
            int shift = index < 0 ? 26 + index : index;
            output += _letters[shift];
        }
        return output;
    }

    private static bool IsPrime(int a) =>
       Enumerable.Range(1, Math.Min(a, _letters.Length)).Where(i => a % i == 0 && _letters.Length % i == 0).Count() == 1;


    public static void Main()
    {
        Console.Write("Podaj tekst:");
        string? input = Console.ReadLine() ?? "";
        Console.WriteLine("Podaj a:");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Podaj b:");
        int b = Convert.ToInt32(Console.ReadLine());
        string cipher = Encipher(input, a, b);
        string decipher = Decipher(cipher, a, b);
        Console.WriteLine($" Input: {input}");
        Console.WriteLine($" Cipher: {cipher}");
        Console.WriteLine($" Decipher: {decipher}");
    }
}



