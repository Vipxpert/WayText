using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emoji
{
    public class UpsideDown
    {
        static Dictionary<char, char> upsideDownMap = new Dictionary<char, char>
    {
        {'a', 'ɐ'},
        {'b', 'q'},
        {'c', 'ɔ'},
        {'d', 'p'},
        {'e', 'ǝ'},
        {'f', 'ɟ'},
        {'g', 'ƃ'},
        {'h', 'ɥ'},
        {'i', 'ı'},
        {'j', 'ɾ'},
        {'k', 'ʞ'},
        {'l', 'l'},
        {'m', 'ɯ'},
        {'n', 'u'},
        {'o', 'o'},
        {'p', 'd'},
        {'q', 'b'},
        {'r', 'ɹ'},
        {'s', 's'},
        {'t', 'ʇ'},
        {'u', 'n'},
        {'v', 'ʌ'},
        {'w', 'ʍ'},
        {'x', 'x'},
        {'y', 'ʎ'},
        {'z', 'z'},
        {'A', '∀'},
        {'B', 'q'},
        {'C', 'Ɔ'},
        {'D', 'p'},
        {'E', 'Ǝ'},
        {'F', 'Ⅎ'},
        {'G', 'פ'},
        {'H', 'H'},
        {'I', 'I'},
        {'J', 'ſ'},
        {'K', 'ʞ'},
        {'L', '˥'},
        {'M', 'W'},
        {'N', 'N'},
        {'O', 'O'},
        {'P', 'Ԁ'},
        {'Q', 'კ'},
        {'R', 'ɹ'},
        {'S', 'S'},
        {'T', '⊥'},
        {'U', '∩'},
        {'V', 'Λ'},
        {'W', 'M'},
        {'X', 'X'},
        {'Y', '⅄'},
        {'Z', 'Z'},
        {'1', 'Ɩ'},
        {'2', 'ᄅ'},
        {'3', 'Ɛ'},
        {'4', 'ㄣ'},
        {'5', 'ϛ'},
        {'6', '9'},
        {'7', 'ㄥ'},
        {'8', '8'},
        {'9', '6'},
        {'0', '0'},
        {' ', ' '},
    };

        public static string MakeUpsideDown(string input)
        {
            char[] result = input.Select(c => upsideDownMap.ContainsKey(c) ? upsideDownMap[c] : c).ToArray();
            Array.Reverse(result);  // Reverse the array to get the upside-down effect
            return new string(result);
        }


        static Dictionary<char, char> downsideUpMap = new Dictionary<char, char>
    {
        {'a', 'ɐ'},
        {'b', 'q'},
        {'c', 'ɔ'},
        {'d', 'p'},
        {'e', 'ǝ'},
        {'f', 'ɟ'},
        {'g', 'ƃ'},
        {'h', 'ɥ'},
        {'i', 'ı'},
        {'j', 'ɾ'},
        {'k', 'ʞ'},
        {'l', '˥'},
        {'m', 'ɯ'},
        {'n', 'u'},
        {'o', 'o'},
        {'p', 'd'},
        {'q', 'b'},
        {'r', 'ɹ'},
        {'s', 's'},
        {'t', 'ʇ'},
        {'u', 'n'},
        {'v', 'ʌ'},
        {'w', 'ʍ'},
        {'x', 'x'},
        {'y', 'ʎ'},
        {'z', 'z'},
        {'A', '∀'},
        {'B', 'q'},
        {'C', 'Ɔ'},
        {'D', 'p'},
        {'E', 'Ǝ'},
        {'F', 'Ⅎ'},
        {'G', 'פ'},
        {'H', 'H'},
        {'I', 'I'},
        {'J', 'ſ'},
        {'K', 'ʞ'},
        {'L', '˥'},
        {'M', 'W'},
        {'N', 'N'},
        {'O', 'O'},
        {'P', 'Ԁ'},
        {'Q', 'კ'},
        {'R', 'ɹ'},
        {'S', 'S'},
        {'T', '⊥'},
        {'U', '∩'},
        {'V', 'Λ'},
        {'W', 'M'},
        {'X', 'X'},
        {'Y', '⅄'},
        {'Z', 'Z'},
        {'1', 'Ɩ'},
        {'2', 'ᄅ'},
        {'3', 'Ɛ'},
        {'4', 'ㄣ'},
        {'5', 'ϛ'},
        {'6', '9'},
        {'7', 'ㄥ'},
        {'8', '8'},
        {'9', '6'},
        {'0', '0'},
        {' ', ' '},  // Keep spaces as they are
    };

        public static string MakeDownsideUp(string input)
        {
            char[] result = input.Select(c => downsideUpMap.ContainsValue(c) ? downsideUpMap.FirstOrDefault(x => x.Value == c).Key : c).ToArray();
            Array.Reverse(result);  // Reverse the array to get the downside-up effect
            return new string(result);
        }
        static Dictionary<char, char> mirrorMap = new Dictionary<char, char>
    {
        {'a', 'ɒ'},
        {'b', 'd'},
        {'c', 'ɔ'},
        {'d', 'b'},
        {'e', 'ɘ'},
        {'f', 'ʇ'},
        {'g', 'ǫ'},
        {'h', 'ʜ'},
        {'i', 'i'},
        {'j', 'ɾ'},
        {'k', 'ʞ'},
        {'l', 'l'},
        {'m', 'ɯ'},
        {'n', 'u'},
        {'o', 'o'},
        {'p', 'q'},
        {'q', 'p'},
        {'r', 'ɹ'},
        {'s', 's'},
        {'t', 'ʇ'},
        {'u', 'n'},
        {'v', 'ʌ'},
        {'w', 'ʍ'},
        {'x', 'x'},
        {'y', 'ʎ'},
        {'z', 'z'},
        {'A', '∀'},
        {'B', 'B'},
        {'C', 'Ɔ'},
        {'D', 'D'},
        {'E', 'Ǝ'},
        {'F', 'Ⅎ'},
        {'G', '⅁'},
        {'H', 'H'},
        {'I', 'I'},
        {'J', 'ſ'},
        {'K', '⋊'},
        {'L', '⅂'},
        {'M', 'W'},
        {'N', 'ᴎ'},
        {'O', 'O'},
        {'P', 'Ԁ'},
        {'Q', 'Ԛ'},
        {'R', 'ɹ'},
        {'S', 'S'},
        {'T', '⊥'},
        {'U', '∩'},
        {'V', 'Λ'},
        {'W', 'M'},
        {'X', 'X'},
        {'Y', 'Y'},
        {'Z', 'Z'},
        {'1', 'Ɩ'},
        {'2', '2'},
        {'3', 'Ɛ'},
        {'4', '4'},
        {'5', '5'},
        {'6', '9'},
        {'7', 'ㄥ'},
        {'8', '8'},
        {'9', '6'},
        {'0', '0'},
        {' ', ' '},  // Keep spaces as they are
    };

        public static string MirrorLeftRight(string input)
        {
            char[] result = input.Select(c => mirrorMap.ContainsKey(c) ? mirrorMap[c] : c).Reverse().ToArray();
            return new string(result);
        }

        public static string Reverse(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
