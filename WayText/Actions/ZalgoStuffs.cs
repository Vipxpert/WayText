
using System.Text;

namespace Emoji
{
    internal class ZalgoStuffs
    {
        static Random random = new Random();
        static string zalgoCharacters = "̴̵̶̷̸̡̢̧̨̜̝̞̟̠̣̤̥̦̩̪̫̬̭̮̯̰̱̲̳̹̺̻̼̽͛ͣͤͥͦͧͨͩͪͫͬͭͮͯ͜͟͢͝͞͠͡";
        public static string ZalgoFyText(string input, decimal intensity)
        {
            if (input != null)
            {
                StringBuilder originalText = new StringBuilder();

                foreach (char c in input)
                {
                    originalText.Append(c);
                    for (int i = 0; i < intensity; i++)
                    {
                        int randomZalgoCharIndex = random.Next(zalgoCharacters.Length);
                        originalText.Append(zalgoCharacters[randomZalgoCharIndex]);
                    }
                }
                return originalText.ToString();
            }
            else
            {
                MessageBox.Show("Empty input");
                return null;
            }
        }
        public static string UnzalgoFyText(string input)
        {
            if (input != null)
            {
                StringBuilder deZalgoFiedText = new StringBuilder();
                foreach (char c in input)
                {
                    // Assuming zalgoCharacters is a string containing all possible Zalgo characters
                    if (!zalgoCharacters.Contains(c))
                    {
                        deZalgoFiedText.Append(c);
                    }
                }
                return deZalgoFiedText.ToString();
            }
            else
            {
                //MessageBox.Show("Empty input");
                return null;
            }
        }


    }
}

//"̸̴̷̵̨̧̡̺͚̟̫̭͉͈̘̝̜͎̳͖̠̞̱̖̦̻̰͕̗̣̩͔͇̙̪͓̌͊̀̍̄͑̽̈́̊̂͌̿̎͋͂̋͛̓͆̏̔̑̃̕̚͘͝b̶̢̛͙͍̤̮̯̥̹̲̼̬͗͒̐̅̉̒̆͐̾̇͜͠ͅ҈҉ًٍّܑ๊ܻ݆ࣩ࣯֟֓֒֘֗ؖؕٞ٘ۛܺ݉݊݅ࣧࣨࣤ͢์๋ືຶ᪴︎;ͣͤͥͦͧͨͩͪͫͬͭͮͯ͟͞͡
//string zalgoCharacters = "̸̴̷̵̨̧̡̺͚̟̫̭͉͈̘̝̜͎̳͖̠̞̱̖̦̻̰͕̗̣̩͔͇̙̪͓̌͊̀̍̄͑̽̈́̊̂͌̿̎͋͂̋͛̓͆̏̔̑̃̕̚͘͝b̶̢̛͙͍̤̮̯̥̹̲̼̬͗͒̐̅̉̒̆͐̾̇͜͠ͅ҈҉ًٍّܑ๊ܻ݆ࣩ࣯֟֓֒֘֗ؖؕٞ٘ۛܺ݉݊݅ࣧࣨࣤ͢์๋ືຶ᪴︎";
