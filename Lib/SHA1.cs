using System;
using System.Linq;

namespace Lib
{
    public class SHA1
    {
        
        /*
        public string Hash(string plainText)
        {
            var bitStringForPlainText = StringToBitString(plainText);
            bitStringForPlainText = AppendZeroToTheEnd(bitStringForPlainText);

            String lengthOfPlainText = IntToBinary(plainText.Length);
            String TextOf512Bit = AppendOriginalMessageLength(bitStringForPlainText, lengthOfPlainText);

            String[] Words = BreakChunkIntoWords(TextOf512Bit);
            Words = Extend80Word(Words);

            var h = new String[5];
            h = InitialVariable();

            return BinaryToHex(Init(Words, h));
        } */
        public string Hash(string plainText)
        {
            var bitStringForPlainText = StringToBitString(plainText);
            bitStringForPlainText = AppendZeroToTheEnd(bitStringForPlainText);

            String lengthOfPlainText = IntToBinary(plainText.Length);
            String AllBitFromPlainText = AppendOriginalMessageLength(bitStringForPlainText, lengthOfPlainText);


            String[] AllChunkOf512Bit = Chunk(AllBitFromPlainText);

            var h = new String[5];
            h = InitialVariable();

            for (int i = 0; i < AllBitFromPlainText.Length / 512; i++)
            {
                String[] Words = BreakChunkIntoWords(AllChunkOf512Bit[i]);
                Words = Extend80Word(Words);

                h = Init(Words, h);
            }

            return BinaryToHex(h);

        }

        public static string[] Chunk(String S)
        {
            String[] chunk = new string[S.Length / 512];

            for (int i = 0; i < S.Length / 512; i++)
            {
                chunk[i] = S.Substring(i * 512, 512);
            }

            return chunk;
        }

        static String[] Words = new String[80];

        public static string[] Init(String[] words, String[] h)
        {
            Words = words;
            String A = "", B = "", C = "", D = "", E = "";
            A = h[0];
            B = h[1];
            C = h[2];
            D = h[3];
            E = h[4];

            for (int i = 0; i < 80; i++)
            {
                String current = Words[i];
                if (i < 20)
                {
                    var k = "01011010100000100111100110011001";

                    var f = OR(AND(B, C), AND(NOT(B), D));

                    var temp = Add(Add(Add(Add(LS(A, 5), f), E), k), current);

                    E = D;
                    D = C;
                    C = LS(B, 30);
                    B = A;
                    A = temp;


                }

                else if (i < 40)
                {
                    var f = XOR(XOR(B, C), D);

                    var k = "01101110110110011110101110100001";

                    var temp = Add(Add(Add(Add(LS(A, 5), f), E), k), current);

                    E = D;
                    D = C;
                    C = LS(B, 30);
                    B = A;
                    A = temp;
                }

                else if (i < 60)
                {
                    var f = OR(OR(AND(B, C), AND(B, D)), AND(C, D));

                    var k = "10001111000110111011110011011100";

                    var temp = Add(Add(Add(Add(LS(A, 5), f), E), k), current);

                    E = D;
                    D = C;
                    C = LS(B, 30);
                    B = A;
                    A = temp;
                }

                else
                {
                    var f = XOR(XOR(B, C), D);

                    var k = "11001010011000101100000111010110";

                    var temp = Add(Add(Add(Add(LS(A, 5), f), E), k), current);

                    E = D;
                    D = C;
                    C = LS(B, 30);
                    B = A;
                    A = temp;
                }
            }

            h[0] = Add(h[0], A);
            h[1] = Add(h[1], B);
            h[2] = Add(h[2], C);
            h[3] = Add(h[3], D);
            h[4] = Add(h[4], E);

            return h;
        }

        public static String AND(String FirstString, String SecondString)
        {
            String Final = "";

            for (int i = 0; i < FirstString.Length; i++)
            {
                if (FirstString[i] == '1' && SecondString[i] == '1')
                {
                    Final += '1';
                }

                else
                {
                    Final += '0';
                }
            }

            return Final;
        }

        public static String OR(String FirstString, String SecondString)
        {
            String Final = "";

            for (int i = 0; i < FirstString.Length; i++)
            {
                if (FirstString[i] == '1' || SecondString[i] == '1')
                {
                    Final += '1';
                }

                else
                {
                    Final += '0';
                }
            }

            return Final;
        }

        public static String NOT(String S)
        {
            String Final = "";

            for (int i = 0; i < S.Length; i++)
            {
                if (S[i] == '1')
                {
                    Final += '0';
                }

                else
                {
                    Final += '1';
                }
            }

            return Final;
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

        public static String LS(String Text, int amount)
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

        public static String Add(String a, String b)
        {
            int number_one = Convert.ToInt32(a, 2);
            int number_two = Convert.ToInt32(b, 2);

            String temp = Convert.ToString(number_one + number_two, 2);

            if (temp.Length > 32)
            {
                temp = temp.Substring(temp.Length - 32, 32);
            }

            else if (temp.Length < 32)
            {
                String Temp = "";
                for (int i = 0; i < 32 - temp.Length; i++)
                {
                    Temp += '0';
                }
                Temp += temp;

                temp = Temp;
            }

            return temp;
        }

        public static string BinaryToHex(String[] h)
        {
            String Hex = "";
            for (int r = 0; r < h.Length; r++)
            {
                String B = h[r];
                for (int i = 0; i < B.Length / 4; i++)
                {
                    String S = B.Substring(i * 4, 4);

                    int ascii = 0;
                    for (int p = 0; p < 4; p++)
                    {
                        int v = (int)Char.GetNumericValue(S[p]);
                        ascii += v * (int)Math.Pow(2, 3 - p);
                    }

                    if (ascii < 10)
                    {
                        Hex += ascii;
                    }

                    else if (ascii == 10) Hex += 'A';
                    else if (ascii == 11) Hex += 'B';
                    else if (ascii == 12) Hex += 'C';
                    else if (ascii == 13) Hex += 'D';
                    else if (ascii == 14) Hex += 'E';
                    else Hex += 'F';

                }
            }

            return Hex;
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