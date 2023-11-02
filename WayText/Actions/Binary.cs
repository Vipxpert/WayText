using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyToolGUI.Actions
{
    internal class Binary
    {
        public static string StringToBinary(string input, int choice = 1)
        {
            try
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input);
                string binary = "";
                foreach (byte b in bytes)
                {
                    string binaryString = Convert.ToString(b, 2).PadLeft(8, '0');
                    switch (choice)
                    {
                        case 1:
                            binaryString = binaryString.Substring(4);
                            break;
                        case 2:
                            binaryString = Convert.ToString(b, 2).PadLeft(8, '0');
                            break;
                        case 3:
                            binaryString = Convert.ToString(b, 16).ToUpper();
                            break;
                    }
                    binary += binaryString + " ";
                }
                return binary.Trim();
            }
            catch
            {
                return input;
            }
            // Remove the trailing space
            
        }

        public static string BinaryToString(string binary)
        {
            string[] binaryBytes = binary.Split(' ');
            string result = "";
            try
            {
                for (int i = 0; i < binaryBytes.Length; i++)
                {
                    string binaryByte = binaryBytes[i];
                    if (binaryByte.Length == 2) // Hexadecimal
                    {
                        result += ((char)Convert.ToByte(binaryByte, 16)).ToString();
                    }
                    else if (binaryByte.Length == 4) // Last 4 bits of Binary
                    {
                        result += Convert.ToByte(binaryByte, 2).ToString();
                    }
                    else // Full Binary
                    {
                        result += System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(binaryByte, 2) });
                    }
                }
                return result;
            }
            catch
            {
                return binary;
            }
        }
    }
}
