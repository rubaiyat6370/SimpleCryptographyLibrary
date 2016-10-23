using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sha_1
{
    public class Function
    {
        String[] Words = new String[80];

        public Function(String[] words, String[] h)
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

            BinaryToHex(h);
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

        public static void BinaryToHex(String[] h)
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

            Console.WriteLine("\n\nHASH " + Hex);
        }
    }
}
