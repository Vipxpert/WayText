using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayTextGUI.Actions
{
    public class StrikeThrough
    {
        public static string ApplyStrikethrough(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                result.Append(c);
                result.Append('\u0336');
            }

            return result.ToString();
        }
    }
}
