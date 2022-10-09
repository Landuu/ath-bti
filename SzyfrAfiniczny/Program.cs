using System.Reflection.Metadata.Ecma335;

namespace SzyfrAfiniczny
{
    public class SzyfrAfiniczny
    {
        //private static readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] _letters = "ABCDKLMNOT".ToCharArray();

        public static bool IsPrime(int n)
        {
            if (n < 1) return false;
            return Enumerable.Range(1, n).Where(x => n % x == 0)
                                 .SequenceEqual(new[] { 1, n });
        }

        public static void Encipher(string text, (int a, int b) key)
        {
            if (!IsPrime(key.a) && key.a >= _letters.Length) return;

            var chars = text.ToCharArray();
            string output = "";
            foreach(var c in chars)
            {
                int charIdex = Array.IndexOf(_letters, c);
                if (charIdex == -1) return;
                int outputIndex = (key.a * charIdex + key.b) % _letters.Length;
                output += _letters[outputIndex];
            }
            Console.WriteLine(output);
        }

        public static void Decipher(string text, (int a, int b) key)
        {
            if (!IsPrime(key.a) && key.a <= _letters.Length) return;

            var chars = text.ToCharArray();
            string output = "";
            foreach (var c in chars)
            {
                int charIdex = Array.IndexOf(_letters, c);
                if (charIdex == -1) return;
                int outputIndex = GetA(key.a) * (charIdex - key.b) % _letters.Length;
                output += _letters[outputIndex % _letters.Length];
            }
            Console.WriteLine(output); 
        }

        public static int GetA(int a)
        {
            for (int i = 1; i <= _letters.Length; i++)
            {
                if ((a * i % _letters.Length) == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static void Main()
        {
            SzyfrAfiniczny.Encipher("KOT", (3, 6));
            SzyfrAfiniczny.Decipher("OAT", (3, 6));
        }
    }
}