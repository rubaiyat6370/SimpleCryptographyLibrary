using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public static class Housekeeping
    {
        public static String StringToBitString(String inputString)
        {

            string BitString = "";
            int number;

            foreach (char c in inputString)
            {
                number = System.Convert.ToInt16(c);

                int quotient;

                string remainder = "";

                while (number >= 1)
                {
                    quotient = number / 2;
                    remainder += (number % 2).ToString();
                    number = quotient;
                }

                for (int i = 0; i < 8 - remainder.Length; i++)
                {
                    BitString += '0';
                }
                for (int i = remainder.Length - 1; i >= 0; i--)
                {
                    BitString = BitString + remainder[i];
                }

            }
            Console.WriteLine("bit string of message: " + BitString);
            Console.WriteLine();
            return BitString;

        }
    }
}
