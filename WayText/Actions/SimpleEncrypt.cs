
namespace Emoji
{
    public class SimpleEncrypt
    {
        Dictionary<char, char> cipherRules = new Dictionary<char, char>();

        public void LoadCipherRules(string ruleFilePath)
        {
            string[] lines = File.ReadAllLines(ruleFilePath);
            foreach (string line in lines)
            {
                if (line.Length >= 3)
                {
                    char originalChar = line[0];
                    char substitutedChar = line[2];
                    cipherRules[originalChar] = substitutedChar;
                }
            }
        }

        public string SimpleCipher(string input)
        {
            char[] result = input.Select(c => cipherRules.ContainsKey(c) ? cipherRules[c] : c).ToArray();
            return new string(result);
        }

        public string SimpleDecipher(string input)
        {
            char[] result = input.Select(c => cipherRules.ContainsValue(c) ? cipherRules.FirstOrDefault(x => x.Value == c).Key : c).ToArray();
            return new string(result);
        }
    }
}
