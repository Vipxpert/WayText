using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emoji
{
    internal class ZalgoStuffs
    {
        static Random random = new Random();
        public static string ZalgoFyText(string input, decimal intensity)
        {
            if (input != null)
            {
                string zalgoCharacters = "̴̵̶̷̸̡̢̧̨̜̝̞̟̠̣̤̥̦̩̪̫̬̭̮̯̰̱̲̳̹̺̻̼̽͛ͣͤͥͦͧͨͩͪͫͬͭͮͯ͜͟͢͝͞͠͡";
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
            }
            MessageBox.Show("Empty input");
            return null;
        }
    }
}
