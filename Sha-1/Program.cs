using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sha_1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter the text:");
            String PlainText = Console.ReadLine();
			var BitStringForPlainText = StringToBitString(PlainText);
			BitStringForPlainText = AppendZeroToTheEnd(BitStringForPlainText);

			String lengthOfPlainText = IntToBinary(PlainText.Length);
			String TextOf512Bit = AppendOriginalMessageLength(BitStringForPlainText, lengthOfPlainText);

			String[] Words = BreakChunkIntoWords(TextOf512Bit);
			Words = Extend80Word(Words);

			String[] h = new String[5];
			h = InitialVariable();

			Function func = new Function(Words,h);
			Console.Read();
		}

		public static String[] InitialVariable()
		{
			String[] h = new String[5] {

				"01100111010001010010001100000001",
				"11101111110011011010101110001001",
				"10011000101110101101110011111110",
				"00010000001100100101010001110110",
				"11000011110100101110000111110000"
			};

			return h;

		}

		public static String StringToBitString(String InputString)
		{
			string BitString = "";
			int ASCII;

			foreach (char c in InputString)
			{
				ASCII = System.Convert.ToInt16(c);

				int quotient;

				string remainder = "";

				while (ASCII >= 1)
				{
					quotient = ASCII / 2;
					remainder += (ASCII % 2).ToString();
					ASCII = quotient;
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

			BitString += '1';

			return BitString;
		}

		public static String IntToBinary(int Number)
		{
			String BitString = "";
			Number *= 8;
			int quotient;

			string remainder = "";

			while (Number >= 1)
			{
				quotient = Number / 2;
				remainder += (Number % 2).ToString();
				Number = quotient;
			}


			for (int i = remainder.Length - 1; i >= 0; i--)
			{
				BitString = BitString + remainder[i];
			}

			return BitString;
		}

		public static String AppendZeroToTheEnd(String InputString)
		{
			int LengthOfMessage = 0;
			for (int i = 0; i < 10; i++)
			{
				if (InputString.Length < (448 + 512 * i))
				{
					LengthOfMessage = 448 + 512 * i;
					break;
				}
			}

			for (int i = InputString.Length; i < LengthOfMessage; i++)
			{
				InputString += '0';
			}

			return InputString;
		}

		public static String AppendOriginalMessageLength(String InputString, String OriginalMessageLength)
		{


			for (int i = 0; i < (64 - OriginalMessageLength.Length); i++)
			{
				InputString += '0';
			}

			InputString += OriginalMessageLength;



			return InputString;

		}

		public static String[] BreakChunkIntoWords(String InputString)
		{
			String[] Words = new String[InputString.Length / 32];

			for (int i = 0; i < InputString.Length / 32; i++)
			{
				for (int j = i * 32; j < (i + 1) * 32; j++)
				{
					Words[i] += InputString[j];
				}
			}

			return Words;
		}

		public static String XOR(String FirstString, String SecondString)
		{
			String FinalString = "";

			for (int i = 0; i < FirstString.Length; i++)
			{
				if (FirstString[i] == SecondString[i])
				{
					FinalString += '0';
				}

				else
				{
					FinalString += '1';
				}
			}

			return FinalString;
		}

		public static String LeftShift(String Text, int amount)
		{
			String Word = "";

			for (int i = amount; i < Text.Length; i++)
			{
				Word += Text[i];
			}

			for (int i = 0; i < amount; i++)
			{
				Word += Text[i];
			}

			return Word;
		}

		public static String[] Extend80Word(String[] InputString)
		{
			String[] Words = new String[80];

			for (int i = 0; i < InputString.Length; i++)
			{
				Words[i] = InputString[i];
			}

			for (int i = InputString.Length; i < 80; i++)
			{
				String Temp = "";
				Temp = XOR(Words[i - 3], Words[i - 8]);
				Temp = XOR(Temp, Words[i - 14]);
				Temp = XOR(Temp, Words[i - 16]);
				Temp = LeftShift(Temp, 1);
				Words[i] = Temp;

			}

			return Words;

		}
	}
}
