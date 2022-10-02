using System.Text.RegularExpressions;

namespace SztfrVig
{
    public class Vig
    {
        private readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private char Shift(char ch, int shift)
        {
            if (shift <= 0) return ch;
            int charIndex = Array.IndexOf(_letters, ch) + shift;
            if (charIndex >= _letters.Length) charIndex -= _letters.Length;
            return _letters[charIndex];
        }

        private char GetChar(Queue<char> textQ, Queue<char> keyQ)
        {
            char c = textQ.Dequeue();
            if (!char.IsLetter(c)) return c;
            char keyChar = keyQ.Dequeue();
            int keyIndex = Array.IndexOf(_letters, keyChar);
            if (keyIndex > 1) return Shift(c, keyIndex);
                else return Shift(c, 0);
        }

        public string Encipher(string text, string key)
        {
            text = text.ToUpper();
            key = key.Replace(" ", "").ToUpper();
            var textQ = new Queue<char>(text);
            var keyQ = new Queue<char>(string.Concat(Enumerable.Repeat(key, text.Length)));
            string output = string.Empty;
            while(textQ.Count > 0){
                output += GetChar(textQ, keyQ);
            }
            return output;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var vig = new Vig();
            string text = "TO JEST BARDZO TAJNY TEKST";
            string key = "TAJNE";

            string output = vig.Encipher(text, key);
            Console.WriteLine(text);
            Console.WriteLine(key);
            Console.WriteLine(output);
        }
    }
}