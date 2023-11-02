
using System.Text;


namespace CopyToolGUI
{
    internal class RandomText
    {
        public static string RandomTextGenerate(string characters,int minLength,int maxLength)
        {
            var random = new Random();
            int length = random.Next(minLength, maxLength + 1);
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                //char c = (char)('a' + random.Next(0, 26)); // Generates a random lowercase letter
                //char[] alphabet = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                string alphabet = characters;
                char c = alphabet[random.Next(0, alphabet.Length)];
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }
    }
}
