
namespace CopyToolGUI
{
    internal class RandomCapital
    {
        static Random random = new Random();
        public static string RandomlyCapitalize(string input)
        {
            char[] characters = input.ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                // Generate a random number (0 or 1) to decide whether to capitalize the character.
                if (random.Next(2) == 0)
                {
                    characters[i] = char.ToLower(characters[i]);
                }
                else
                {
                    characters[i] = char.ToUpper(characters[i]);
                }
            }
            return new string(characters);
        }
    }
}
