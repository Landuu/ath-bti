using System.Text.RegularExpressions;

namespace SztfrVig
{
    public static class Vig
    {
        private static readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private static char Shift(char ch, int shift)
        {
            if (shift <= 0) return ch;
            int charIndex = Array.IndexOf(_letters, ch) + shift;
            if (charIndex >= _letters.Length) charIndex -= _letters.Length;
            return _letters[charIndex];
        }

        private static char GetChar(Queue<char> textQ, Queue<char> keyQ)
        {
            char c = textQ.Dequeue();
            if (!char.IsLetter(c)) return c;
            char keyChar = keyQ.Dequeue();
            int keyIndex = Array.IndexOf(_letters, keyChar);
            if (keyIndex > 1) return Shift(c, keyIndex);
                else return Shift(c, 0);
        }

        public static string Encipher(string text, string key)
        {
            if (key.Length == 0) return text;
            text = text.ToUpper();
            key = key.Replace(" ", "").ToUpper();
            var textQ = new Queue<char>(text);
            var keyQ = new Queue<char>(string.Concat(Enumerable.Repeat(key, text.Length)));
            string output = string.Empty;
            while(textQ.Count > 0) {
                output += GetChar(textQ, keyQ);
            }
            return output;
        }

        private static string GetDecipherKey(string key)
        {
            string deKey = "";
            foreach (char c in key)
            {
                int charIndex = Array.IndexOf(_letters, c);
                int nextIndex = (_letters.Length - charIndex) % _letters.Length;
                deKey += _letters[nextIndex];
            }
            return deKey;
        }

        public static string Decipher(string text, string key)
        {
            string deKey = GetDecipherKey(key.ToUpper().Replace(" ", ""));
            return Encipher(text, deKey);
        }
    }

    public class Program
    {
        public static void Main()
        {
            /*
                Console.Write("Podaj tekst do zaszyfrowania: ");
                string text = Console.ReadLine() ?? "";
                Console.Write("Podaj klucz:");
                string key = Console.ReadLine() ?? "";

                string encrypted = Vig.Encipher(text, key);
                string decrypted = Vig.Decipher(encrypted, key);
                Console.WriteLine();
                Console.Write("Zaszyfrowany tekst:");
                Console.WriteLine(encrypted);
                Console.Write("Odszyfrowany tekst:");
                Console.WriteLine(decrypted);
            */

            Console.Write("Podaj tekst do odszyfrowania: ");
            string text = Console.ReadLine() ?? "";
            Console.Write("Podaj klucz:");
            string key = Console.ReadLine() ?? "";

            string decrypted = Vig.Decipher(text, key);
            Console.WriteLine();
            Console.Write("Odszyfrowany tekst:");
            Console.WriteLine(decrypted);
        }
    }
}