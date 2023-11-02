using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emoji
{
    internal class RegexGenerator
    {
        private static Random random = new Random();
        private StringBuilder stringBuilder = new StringBuilder();
        private System.Text.RegularExpressions.Regex regex;
        private int minLength;
        private int maxLength;

        public RegexGenerator(System.Text.RegularExpressions.Regex regex, int minLength, int maxLength)
        {
            this.regex = regex;
            this.minLength = Math.Max(0, minLength);
            this.maxLength = Math.Max(minLength, maxLength);
        }

        public void BuildRandomString()
        {
            while (true)
            {
                int length = random.Next(minLength, maxLength + 1);
                GenerateRandomString(length);
                string currentString = stringBuilder.ToString();

                if (regex.IsMatch(currentString))
                {
                    Console.WriteLine("Current string: " + currentString);
                    return;
                }

                // If the generated string doesn't match the regex, adjust the length
                AdjustStringLength(currentString);
            }
        }
        private void GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
        }

        private void AdjustStringLength(string currentString)
        {
            int currentLength = currentString.Length;

            if (currentLength < minLength)
            {
                int additionalChars = minLength - currentLength;
                GenerateRandomString(additionalChars);
            }
            else if (currentLength > maxLength)
            {
                int excessChars = currentLength - maxLength;
                stringBuilder.Remove(maxLength, excessChars);
            }
        }
        public string GetRandomString()
        {
            return stringBuilder.ToString();
        }
    }
}
