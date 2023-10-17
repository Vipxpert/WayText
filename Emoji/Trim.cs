using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
