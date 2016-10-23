using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5
{
    class Program
    {
        static void Main(string[] args)
        {
            String PlainText = "Mumu";

            String BitString = StringToBitString(PlainText);
            //BitString = BitString + "mumu";
            Console.WriteLine(BitString + " " + BitString.Length);
            BitString= AppendZeroToTheEnd(BitString);

            Console.WriteLine(BitString + " " + BitString.Length);

            BitString = AppendOriginalMessageLength(BitString, IntToBinary(PlainText.Length));

	    String[] words = BreakChunkIntoWords(BitString);

	    MainLoop(words);
            
            Console.ReadKey();
        }

        public static int ShiftingAmount(int i)
        {
            int[] s =
            {
                7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
                5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
                4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
                6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21
            };

            return s[i];

        }

        public static String KVariable( int i)
        {
            String[] k =
            {
                "d76aa478", "e8c7b756", "242070db", "c1bdceee",
                "f57c0faf", "4787c62a", "a8304613", "fd469501",
                "698098d8", "8b44f7af", "ffff5bb1", "895cd7be",
                "6b901122", "fd987193", "a679438e", "49b40821",
                "f61e2562", "c040b340", "265e5a51", "e9b6c7aa",
                "d62f105d", "02441453", "d8a1e681", "e7d3fbc8",
                "21e1cde6", "c33707d6", "f4d50d87", "455a14ed",
                "a9e3e905", "fcefa3f8", "676f02d9", "8d2a4c8a",
                "fffa3942", "8771f681", "6d9d6122", "fde5380c",
                "a4beea44", "4bdecfa9", "f6bb4b60", "bebfbc70",
                "289b7ec6", "eaa127fa", "d4ef3085", "04881d05",
                "d9d4d039", "e6db99e5", "1fa27cf8", "c4ac5665",
                "f4292244", "432aff97", "ab9423a7", "fc93a039",
                "655b59c3", "8f0ccc92", "ffeff47d", "85845dd1",
                "6fa87e4f", "fe2ce6e0", "a3014314", "4e0811a1",
                "f7537e82", "bd3af235", "2ad7d2bb", "eb86d391"

            };

            return k[i];
        }

        public static String InitialVariable(int i)
        {
            String[] initial4 = { "67452301", "efcdab89", "98badcfe", "10325476" };

            return initial4[i];
        }

        public static String StringToBitString(String inputString)
        {
            String bitString = "";

            foreach (char c in inputString)
            {
                int asciiValueOfCharacter = Convert.ToInt16(c);

                String remainder = "";

                while (asciiValueOfCharacter > 0)
                {
                    int quotient = asciiValueOfCharacter / 2;
                    remainder += (asciiValueOfCharacter % 2).ToString();
                    asciiValueOfCharacter = quotient;
                }

                for (int i = 0; i<(8-remainder.Length); i++)
                {
                    bitString += '0';
                }
                //Console.WriteLine(remainder +  " "+ remainder.Length);
                
                for (int i = remainder.Length - 1; i >= 0; i--)
                {
                    bitString = bitString + remainder[i];
                }

            }

            bitString += '1';

            return bitString;
        }

        public static String AppendZeroToTheEnd(String inputString)
        {
            int lengthOfMessage = 0;
           
            for (;;)
            {
                int i = 0;
                if (inputString.Length < (448 + 512 * i))
                {
                    lengthOfMessage = 448 + 512 * i;
                    break;
                }

                i++;
            }

            for (int i = inputString.Length; i < lengthOfMessage; i++)
            {
                inputString += '0';
            }

            return inputString;
        }

        public static String AppendOriginalMessageLength(String inputString, String messageLength)
        {


            for (int i = 0; i < (64 - messageLength.Length); i++)
            {
                inputString += '0';
            }

            inputString += messageLength;



            return inputString;

        }

        public static String IntToBinary(int number)
        {
            String bitString = "";
            
            number *= 8;
            
            int quotient;

            string remainder = "";

            while (number >= 1)
            {
                quotient = number / 2;
                remainder += (number % 2).ToString();
                number = quotient;
            }


            for (int i = remainder.Length - 1; i >= 0; i--)
            {
                bitString = bitString + remainder[i];
            }

            return bitString;
        }

        public static String[] BreakChunkIntoWords(String inputString)
        {
            String[] Words = new String[inputString.Length / 32];

            for (int i = 0; i < inputString.Length / 32; i++)
            {
                for (int j = i * 32; j < (i + 1) * 32; j++)
                {
                    Words[i] += inputString[j];
                }
            }

            return Words;
        }

	public static String BinaryToHex(String b)
	{
		String hex = "";

		for (int i = 0; i < b.Length / 4; i++)
		{
			String s = b.Substring(i * 4, 4);

			int ascii = 0;
			for (int p = 0; p < 4; p++)
			{
				int v = (int)Char.GetNumericValue(s[p]);
				ascii += v * (int)Math.Pow(2, 3 - p);
			}

			if (ascii < 10)
			{
				hex += ascii;
			}

			else if (ascii == 10) hex += 'A';
			else if (ascii == 11) hex += 'B';
			else if (ascii == 12) hex += 'C';
			else if (ascii == 13) hex += 'D';
			else if (ascii == 14) hex += 'E';
			else hex += 'F';

		}

		String temp = "";
		for (int i = hex.Length; i <= 8; i++)
		{
			temp += '0';
		}
		temp += hex;

		return temp;
	}

	public static String IntTo4BitBinary(int number)
	{
		var bitString = "";

		var remainder = "";

		while (number >= 1)
		{
			var quotient = number / 2;
			remainder += (number % 2).ToString();
			number = quotient;
		}

		for (var i = 0; i < 4 - remainder.Length; i++)
		{
			bitString += '0';
		}


		for (var i = remainder.Length - 1; i >= 0; i--)
		{
			bitString = bitString + remainder[i];
		}

		return bitString;

	}

	public static String HexToBinary(String hex)
	{
		var binary = "";

		foreach (char variable in hex)
		{
			if (variable == 'a') binary += IntTo4BitBinary(10);
			else if (variable == 'b') binary += IntTo4BitBinary(11);
			else if (variable == 'c') binary += IntTo4BitBinary(12);
			else if (variable == 'd') binary += IntTo4BitBinary(13);
			else if (variable == 'e') binary += IntTo4BitBinary(14);
			else if (variable == 'f') binary += IntTo4BitBinary(15);
			else binary += IntTo4BitBinary((int)Char.GetNumericValue(variable));
		}

		return binary;
	}

	public static String AND(String firstString, String secondString)
	{
		String final = "";

		for (int i = 0; i < firstString.Length; i++)
		{
			if (firstString[i] == '1' && secondString[i] == '1')
			{
				final += '1';
			}

			else
			{
				final += '0';
			}
		}

		return final;
	}

	public static String OR(String firstString, String secondString)
	{
		String final = "";

		for (int i = 0; i < firstString.Length; i++)
		{
			if (firstString[i] == '1' || secondString[i] == '1')
			{
				final += '1';
			}

			else
			{
				final += '0';
			}
		}

		return final;
	}

	public static String NOT(String s)
	{
		String final = "";

		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == '1')
			{
				final += '0';
			}

			else
			{
				final += '1';
			}
		}

		return final;
	}

	public static String XOR(String firstString, String secondString)
	{
		String finalString = "";

		for (int i = 0; i < firstString.Length; i++)
		{
			if (firstString[i] == secondString[i])
			{
				finalString += '0';
			}

			else
			{
				finalString += '1';
			}
		}

		return finalString;
	}

	public static String LS(String text, int amount)
	{
		String word = "";

		for (int i = amount; i < text.Length; i++)
		{
			word += text[i];
		}

		for (int i = 0; i < amount; i++)
		{
			word += text[i];
		}

		return word;
	}

	public static String Add(String a, String b)
	{
		int numberOne = Convert.ToInt32(a, 2);
		int numberTwo = Convert.ToInt32(b, 2);

		String temp = Convert.ToString(numberOne + numberTwo, 2);

		String tempString = "";
		if (temp.Length > 32)
		{
			temp = temp.Substring(temp.Length - 32, 32);
		}

		else if (temp.Length < 32)
		{
			for (int i = 0; i < 32 - temp.Length; i++)
			{
				tempString += '0';
			}
			tempString += temp;

			temp = tempString;
		}

		return temp;
	}

	public static void MainLoop(String[] m)
	{
		String[] initial = InitialVariableForMd5();
		for (int k = 0; k < 4; k++)
		{
			initial[k] = HexToBinary(initial[k]);
		}
		var a = initial[0];
		var b = initial[1];
		var c = initial[2];
		var d = initial[3];

		for (int i = 0; i < 64; i++)
		{
			//Console.WriteLine("Loop "+i);
			string f;
			int g;

			if (i < 16)
			{
				f = OR(AND(b, c), AND(NOT(b), d));
				g = i;
			}

			else if (i < 32)
			{
				f = OR(AND(d, b), AND(NOT(d), c));
				g = (5 * i + 1) % 16;
			}

			else if (i < 48)
			{
				f = XOR(b, XOR(c, d));
				g = (3 * i + 5) % 16;
			}

			else
			{
				f = XOR(c, OR(b, NOT(d)));
				g = (7 * i) % 16;
			}

			var temp = d;
			d = c;
			c = b;
			b = Add(b, LS(Add(a, Add(f, Add(HexToBinary(Kvariable(i)), m[g]))), S(i)));
			a = temp;

		}

		initial[0] = Add(initial[0], a);
		initial[1] = Add(initial[1], b);
		initial[2] = Add(initial[2], c);
		initial[3] = Add(initial[3], d);

		String final = "";

		foreach (var s in initial)
		{
			final += s;
		}

		final = BinaryToHex(final);

		Console.WriteLine("\n\t\t\tCharecter Digest \n\n\t\t" + final);
	}

    }
}
