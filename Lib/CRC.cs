using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class CRC
    {
        List<int> powerList = new List<int>();

       public string executeCrc(string input, int crcLength)
        {
            var crcDictionary = new Dictionary<int, string>
            {
                {1,"x+1"}, 
                {4,"x^4+x+1"}, 
                {8,"x^8+x^7+x^6+x^4+x^2+1"}, 
                {12,"x^12+x^11+x^3+x^2+x+1"},
                {16,"x^16+x^15+x^2+1"},
                {24,"x^24+x^22+x^20+x^19+x^18+x^16+x^14+x^13+x^11+x^10+x^8+x^7+x^6+x^3+x+1"},
                {30,"x^30+x^29+x^21+x^20+x^15+x^13+x^12+x^11+x^8+x^7+x^6+x^2+x+1"},
                {32,"x^32+x^26+x^23+x^22+x^16+x^12+x^11+x^10+x^8+x^7+x^5+x^4+x^2+x+1"}
            };

            var crcEqn = crcDictionary[crcLength];
            var returnValue = GetValue(crcEqn, input);

            var crcCode = new StringBuilder();

            foreach (var item in returnValue)
            {
                crcCode.Append(item.ToString());
            }

            var finalCrc = crcCode.ToString();
            if (finalCrc.Length == crcLength)
            {
                return finalCrc;
            }
            else
            {
                String temp = "";
                for (int i = finalCrc.Length; i < crcLength; i++)
                {
                    temp += '0';
                }

                finalCrc = temp + finalCrc;
            }
            return finalCrc;
        }

        private IEnumerable<char> GetValue(string polynomial, string text)
        {
            var generatedPolynomial = PolynomialToBinary(polynomial);

            var extendedPart = "";
            for (int i = 0; i < powerList.Max(); i++)
//            for (int i = 0; i < generatedPolynomial.Length-1; i++)
            {
                extendedPart = extendedPart + "0";
            }
            var message = (Housekeeping.StringToBitString(text) + extendedPart);
            var messageCopy = message.ToList();

            var polyInBinary = generatedPolynomial.ToList();

            while (messageCopy.Count >= polyInBinary.Count)
            {
                int i = 0;
                while (i < polyInBinary.Count)
                {
                    if (messageCopy[i] == polyInBinary[i])
                    {
                        messageCopy[i] = '0';
                    }
                    else
                    {
                        messageCopy[i] = '1';
                    }
                    i++;
                }
                
                int log = 0;
                for (int j = 0; j < polyInBinary.Count - 1; j++)
                {
                    log++;
                    if (log > polyInBinary.Count)
                    {
                        break;
                    }

                    if (messageCopy[j] == '1')
                    {
                        break;
                    }

                    else
                    {
                        messageCopy.RemoveAt(j);
                        j--;
                    }
                }
            }

            return messageCopy;
        }

        private String PolynomialToBinary(string polynomial)
        {
             
            //string motherString = "";
/*
            var powerList = new List<int>();
*/
            var partsOfEqn= polynomial.Split('+');
            foreach (var part in partsOfEqn)
            {
                bool allDigits = part.All(char.IsDigit);
                if (allDigits)
                {
                    powerList.Add(0);
                }

                var t = part.Split('^');
                if (t.Length == 1 && t[0] == "x" )
                {
                    powerList.Add(1);
                }
                if (t.Length > 1)
                {
                    powerList.Add(Convert.ToInt32(t[1]));
                }
                
            }
            var generatedPolynomialList = new List<string>();
            for (int i = 0; i <= powerList.Max(); i++)
            {
                generatedPolynomialList.Add("0");
            }

            foreach (var i in powerList)
            {
                generatedPolynomialList[i] = "1";
            }
            generatedPolynomialList.Reverse();
            var generatedPolynomial = string.Join("", generatedPolynomialList.ToArray());
            //var message = StringToBitString(text).ToList();
            //var messageCopy = message;
            return generatedPolynomial;
        }
    }
}
