
using System.Text;
namespace Emoji
{
    public class UpsideDown
    {
        static string[][] directionalEmoji = { new string[] { "👉", "\u0001" }, new string[] { "👈", "\u0002" } };
        static string[][] reversedDirectionalEmoji = new string[][] { new string[] { "👈", "\u0001" }, new string[] { "👉", "\u0002" } };
        static Dictionary<string, string> upsideDownMap = new Dictionary<string, string>
    {
        {"a", "ɐ"},
    {"b", "q"},
    {"c", "ɔ"},
    {"d", "p"},
    {"e", "ǝ"},
    {"f", "ɟ"},
    {"g", "ƃ"},
    {"h", "ɥ"},
    {"i", "ᴉ"},
    {"j", "ɾ"},
    {"k", "ʞ"},
    {"l", "l"},
    {"m", "ɯ"},
    {"n", "u"},
    {"o", "o"},
    {"p", "d"},
    {"q", "b"},
    {"r", "ɹ"},
    {"s", "s"},
    {"t", "ʇ"},
    {"u", "n"},
    {"v", "ʌ"},
    {"w", "ʍ"},
    {"x", "x"},
    {"y", "ʎ"},
    {"z", "z"},
    {"A", "∀"},
    {"B", "ᗺ"},
    {"C", "Ɔ"},
    {"D", "ᗡ"},
    {"E", "Ǝ"},
    {"F", "Ⅎ"},
    {"G", "פ"},
    {"H", "H"},
    {"I", "I"},
    {"J", "ſ"},
    {"K", "ʞ"},
    {"L", "˥"},
    {"M", "W"},
    {"N", "N"},
    {"O", "O"},
    {"P", "Ԁ"},
    {"Q", "Ỏ"},
    {"R", "ᴚ"},
    {"S", "S"},
    {"T", "⊥"},
    {"U", "∩"},
    {"V", "Λ"},
    {"W", "M"},
    {"X", "X"},
    {"Y", "⅄"},
    {"Z", "Z"},
    {"1", "Ɩ"},
    {"2", "ᄅ"},
    {"3", "Ɛ"},
    {"4", "ㄣ"},
    {"5", "ϛ"},
    {"6", "9"},
    {"7", "ㄥ"},
    {"8", "8"},
    {"9", "6"},
    {"0", "0"},
    {" ", " "},
    {"&", "⅋" },
    {",", "'"},
    {".", "˙" },
    {";", "؛" },
    {"_", "‾" },
    {"⁀", "‿" },
    {"¿", "?" },
    {"!", "¡" },
    {"\"", "„" },
    {"👉", "👈" },
    };

        //This approach doesn't work because {"b","d"} and {"d","b"} co-exist
        /*public static string MakeUpsideDown(string input)
        {
            string result = input;

            foreach (var pair in upsideDownMap)
            {
                string original = pair.Key;
                string reversed = pair.Value;
                // Use regular expressions to replace original and reversed forms
                result = Regex.Replace(result, Regex.Escape(original), "ㅤ");
                result = Regex.Replace(result, Regex.Escape(reversed), original);
                result = Regex.Replace(result, "ㅤ", reversed);
            }

            return Reverse(result);
        }*/

        /*public static string MakeDownsideUp(string converted)
        {
            string result = converted;

            foreach (var pair in upsideDownMap)
            {
                string original = pair.Key;
                string reversed = pair.Value;

                // Use regular expressions to replace original and reversed forms when undoing
                result = Regex.Replace(result, Regex.Escape(reversed), "ㅤ");
                result = Regex.Replace(result, Regex.Escape(original), reversed);
                result = Regex.Replace(result, "ㅤ", original);
            }

            return Reverse(result);
        }*/

        public static string MakeUpsideDown(string input)
        {
            StringBuilder result = new StringBuilder(input);
            var swappedChars = new HashSet<char>();

            foreach (var pair in upsideDownMap)
            {
                char original = pair.Key[0];
                char reversed = pair.Value[0];

                if (!swappedChars.Contains(original) && !swappedChars.Contains(reversed))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == original)
                        {
                            result[i] = reversed;
                        }
                        else if (result[i] == reversed)
                        {
                            result[i] = original;
                        }
                    }

                    swappedChars.Add(original);
                    swappedChars.Add(reversed);
                }
            }

            foreach (var pair in directionalEmoji)
            {
                result = result.Replace(pair[0], pair[1]);
            }
            result = new StringBuilder(Reverse(result.ToString()));
            foreach (var pair in reversedDirectionalEmoji)
            {
                result = result.Replace(pair[1], pair[0]);
            }

            return result.ToString();
        }


        public static string MakeDownsideUp(string input)
        {
            StringBuilder result = new StringBuilder(input);
            var swappedChars = new HashSet<char>();

            foreach (var pair in upsideDownMap)
            {
                char original = pair.Key[0];
                char reversed = pair.Value[0];

                if (!swappedChars.Contains(original) && !swappedChars.Contains(reversed))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == original)
                        {
                            result[i] = reversed;
                        }
                        else if (result[i] == reversed)
                        {
                            result[i] = original;
                        }
                    }

                    swappedChars.Add(original);
                    swappedChars.Add(reversed);
                }
            }

            foreach (var pair in directionalEmoji)
            {
                result = result.Replace(pair[0], pair[1]);
            }
            result = new StringBuilder(Reverse(result.ToString()));
            foreach (var pair in reversedDirectionalEmoji)
            {
                result = result.Replace(pair[1], pair[0]);
            }

            return result.ToString();
        }




        static Dictionary<string, string> mirrorMap = new Dictionary<string, string>
{
    {"a", "ɒ"},
    {"b", "d"},
    {"c", "ɔ"},
    {"d", "b"},
    {"e", "ɘ"},
    {"f", "ʇ"},
    {"g", "ϱ"},
    {"h", "⑁"},
    {"i", "i"},
    {"j", "ᒑ"},
    {"k", "ʞ"},
    {"l", "l"},
    {"m", "m"},
    {"n", "n"},
    {"o", "o"},
    {"p", "q"},
    {"q", "p"},
    {"r", "ɿ"},
    {"s", "ƨ"},
    {"t", "ɟ"},
    {"u", "u"},
    {"v", "v"},
    {"w", "w"},
    {"x", "x"},
    {"y", "γ"},
    {"z", "z"},
    {"A", "A"},
    {"B", "ᗺ"},
    {"C", "Ɔ"},
    {"D", "ᗡ"},
    {"E", "Ǝ"},
    {"F", "ꟻ"},
    {"G", "ວ"},
    {"H", "H"},
    {"I", "I"},
    {"J", "ᒐ"},
    {"K", "ꓘ"},
    {"L", "⅃"},
    {"M", "M"},
    {"N", "И"},
    {"O", "O"},
    {"P", "ᑫ"},
    {"Q", "Ϙ"},
    {"R", "Я"},
    {"S", "Ƨ"},
    {"T", "T"},
    {"U", "U"},
    {"V", "V"},
    {"W", "W"},
    {"X", "X"},
    {"Y", "Y"},
    {"Z", "Z"},
    {"1", "Ɩ"},
    {"2", "2"},
    {"3", "Ɛ"},
    {"4", "4"},
    {"5", "5"},
    {"6", "9"},
    {"7", "ㄥ"},
    {"8", "8"},
    {"9", "6"},
    {"0", "0"},
    {" ", " "},
    {"?","⸮"},
    {"(", ")" },
    {"〈", "〉" },
    {"⟪", "⟫" },
    {"⟮", "⟯" },
    {"❪", "❫" },
    {"❬", "❭" },
            {"＜", "＞" },
    {"❰", "❱" },
    {"❴", "❵" },
    {"{", "}"},
    {"[", "]"},
            {"╱", "╲" },
            {"´", "｀" },
            {"⋰", "⋱" },
            {"T᷄", "T᷅" },
            {"T᷅", "T᷅" },
    {"/", "\\" },
    {"／", "＼" },
    {"≦", "≧" },
    {"├", "┤" },
    {"<", ">" },
    {"«", "»" },
    {"༼", "༽" },
    {"‹", "›" },
    {"᚛", "᚜" },
    {"⌈", "⌉" },
{ "╭", "╮" },
    { "⌊", "⌋" },
    {"┌", "┐"},
    {"└", "┘" },
         /*{"👉", "👈" },
         {"👈", "👉" },*/
};

        public static string MirrorLeftRight(string input)
        {
            StringBuilder result = new StringBuilder(input);
            var swappedChars = new HashSet<char>();

            foreach (var pair in mirrorMap)
            {
                char original = pair.Key[0];
                char reversed = pair.Value[0];

                if (!swappedChars.Contains(original) && !swappedChars.Contains(reversed))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == original)
                        {
                            result[i] = reversed;
                        }
                        else if (result[i] == reversed)
                        {
                            result[i] = original;
                        }
                    }

                    swappedChars.Add(original);
                    swappedChars.Add(reversed);
                }
            }

            foreach (var pair in directionalEmoji)
            {
                result = result.Replace(pair[0], pair[1]);
            }
            result = new StringBuilder(Reverse(result.ToString()));
            foreach (var pair in reversedDirectionalEmoji)
            {
                result = result.Replace(pair[1], pair[0]);
            }

            return result.ToString();
        }

        public static string UnMirrorLeftRight(string input)
        {
            StringBuilder result = new StringBuilder(input);
            var swappedChars = new HashSet<char>();

            foreach (var pair in mirrorMap)
            {
                char original = pair.Key[0];
                char reversed = pair.Value[0];

                if (!swappedChars.Contains(original) && !swappedChars.Contains(reversed))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] == original)
                        {
                            result[i] = reversed;
                        }
                        else if (result[i] == reversed)
                        {
                            result[i] = original;
                        }
                    }

                    swappedChars.Add(original);
                    swappedChars.Add(reversed);
                }
            }

            foreach (var pair in directionalEmoji)
            {
                result = result.Replace(pair[0], pair[1]);
            }
            result = new StringBuilder(Reverse(result.ToString()));
            foreach (var pair in reversedDirectionalEmoji)
            {
                result = result.Replace(pair[1], pair[0]);
            }

            return result.ToString();
        }


        public static string Reverse(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
