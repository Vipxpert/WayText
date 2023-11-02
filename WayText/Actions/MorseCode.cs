using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyToolGUI.Actions
{
    internal class MorseCode
    {
        private static readonly Dictionary<char, string> charToMorse = new Dictionary<char, string>
    {
        {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."}, {'G', "--."}, {'H', "...."},
        {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."},
        {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
        {'Y', "-.--"}, {'Z', "--.."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
        {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"}, {' ', "/"},
        {',', "--..--"}, {'.', ".-.-.-"}, {'?', "..--.."}, {'\'', ".----."}, {'!', "-.-.--"},
        {'/', "-..-."}, {'(', "-.--."}, {')', "-.--.-"}, {'&', ".-..."}, {':', "---..."},
        {';', "-.-.-."}, {'=', "-...-"}, {'+', ".-.-."}, {'-', "-....-"}, {'_', "..--.-"},
        {'"', ".-..-."}, {'$', "...-..-"}, {'@', ".--.-."}
    };

        public static string TextToMorse(string text)
        {
            text = text.ToUpper();
            StringBuilder morseCode = new StringBuilder();

            foreach (char c in text)
            {
                if (charToMorse.ContainsKey(c))
                {
                    morseCode.Append(charToMorse[c]);
                    morseCode.Append(' ');
                }
                else if (c == '\n' || c == '\r')
                {
                    morseCode.Append('\n');
                }
            }

            return morseCode.ToString().Trim();
        }

        public static string MorseToText(string morse)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                string[] words = morse.Split(new[] { " / " }, StringSplitOptions.None);

                foreach (string word in words)
                {
                    string[] symbols = word.Split(' ');

                    foreach (string symbol in symbols)
                    {
                        if (charToMorse.ContainsValue(symbol))
                        {
                            char character = charToMorse.FirstOrDefault(x => x.Value == symbol).Key;
                            text.Append(character);
                        }
                        else if (symbol == "\n")
                        {
                            text.Append("\r\n");
                        }
                    }

                    text.Append(' ');
                }

                return text.ToString().Trim();
            }
            catch
            {
                return morse;
            }
        }
    }
}
