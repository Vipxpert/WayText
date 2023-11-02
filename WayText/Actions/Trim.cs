
using System.Text;
using System.Text.RegularExpressions;

namespace Emoji
{
    internal class Trim
    {
        public static string TrimString(string inputSentence, string stringToRemove)
        {
            // Use the Replace method to remove the specified substring.
            string result = inputSentence.Replace(stringToRemove, "");
            return result;
        }

        public static string TrimCharacter(string input, string charactersToRemove)
        {
            StringBuilder result = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (charactersToRemove.IndexOf(c) == -1)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public static string TrimLeadingNumeric(string input)
        {
            // Use a regular expression to remove leading numeric characters
            string result = Regex.Replace(input, @"^\d+", "");

            return result;
        }

        public static string TrimDuplicates(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder result = new StringBuilder();
            HashSet<char> seenCharacters = new HashSet<char>();

            foreach (char c in input)
            {
                if (!seenCharacters.Contains(c))
                {
                    seenCharacters.Add(c);
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
