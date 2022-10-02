namespace SzyfrCezara

{

    public class CipherEncoder
    {
        private readonly char[] _letters;
        private readonly int _shift;

        public CipherEncoder(int shift = 7)
        {
            string charset = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWYZŹŻ";
            _letters = charset.ToCharArray();
            _shift = shift;
        }

        private char Shift(char ch, bool forward)
        {
            if (!char.IsLetter(ch))
                return ch;

            int charIndex = forward ? Array.IndexOf(_letters, ch) +
                            _shift : Array.IndexOf(_letters, ch) - _shift;

            if (forward)
            {
                if (charIndex >= _letters.Length)
                    charIndex -= _letters.Length;
            }
            else
            {
                if (charIndex < 0)
                    charIndex += _letters.Length;
            }

            return _letters[charIndex];
        }

        public string ShiftLoop(string message, bool forward)
        {
            message = message.Trim().ToUpper();
            char[] messageChars = message.ToCharArray();
            for (int i = 0; i < messageChars.Length; i++)
                messageChars[i]
                = Shift(messageChars[i], forward);
            return new string(messageChars);
        }

        public string Encode(string message) => ShiftLoop(message, true);

        public string Decode(string message) => ShiftLoop(message, false);
    }

    public static class Program
    {
        public static void Main()
        {
            var ce = new CipherEncoder(3);

            string msg = "MĘŻNY BĄDŹ, CHROŃ PUŁK TWÓJ I SZEŚĆ FLAG";
            string encoded = ce.Encode(msg);
            string decoded = ce.Decode(encoded);

            Console.WriteLine(msg);
            Console.WriteLine(encoded);
            Console.WriteLine(decoded);
        }
    }

}