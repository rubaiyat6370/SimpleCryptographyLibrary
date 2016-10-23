using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            String PlainText = "1101011100101000";
            String Key = "0100101011110101";
            String CipherText = Encryption(PlainText, Key);
            String FidBack = Decryption(CipherText, Key);

            Console.WriteLine("PlainText : \t" + PlainText);

            Console.WriteLine("CipherTect : \t" + CipherText);

            Console.WriteLine("FidBack : \t" + FidBack);


            Console.Read();
        }

        public static String Decryption(String CipherText, String Key)
        {
            String[] K = GenerateKey(Key);

            String S = XOR(CipherText, K[2]);

            S = Swap(S);

            S = DecryptionSubstitution(S);

            S = XOR(S, K[1]);

            S = MultiplicationForDecryption(S);

            S = Swap(S);
            S = DecryptionSubstitution(S);
            S = XOR(S, K[0]);

            return S;
        }

        public static String Encryption(String PlainText, String Key)
        {
            String[] K = GenerateKey(Key);
            String S = XOR(PlainText, K[0]);
            S = EncryptionSubstitution(S);							//Round 1 Start
            S = Swap(S);


            /*
             * Matrics Multiplication
             */

            S = MultiplicationForEncryption(S);


            S = XOR(S, K[1]);								//Round 1 End

            //Console.WriteLine(S);

            S = EncryptionSubstitution(S);							//Final Round Start
            S = Swap(S);
            S = XOR(S, K[2]);							//Final Round End

            return S;
        }

        public static String Swap(String SixtenBit)
        {
            String s = "";
            s += SixtenBit.Substring(0, 4) + SixtenBit.Substring(12, 4);
            s += SixtenBit.Substring(8, 4) + SixtenBit.Substring(4, 4);

            return s;
        }

        public static String EncryptionSubstitution(String input)
        {
            var output = "";
            var sBoxInput = new[]
			{
				"0000", "0001", "0010", "0011",
				"0100", "0101", "0110", "0111",
				"1000", "1001", "1010", "1011",
				"1100", "1101", "1110", "1111"
			};

            var sBoxOutput = new[]
			{
				"1001", "0100", "1010", "1011",
				"1101", "0001", "1000", "0101",
				"0110", "0010", "0000", "0011",
				"1100", "1110", "1111", "0111"
			};

            for (var j = 0; j < input.Length / 4; j++)
            {
                var s = input.Substring(j * 4, 4);
                for (var i = 0; i < 16; i++)
                {
                    if (sBoxInput[i] == s)
                    {
                        output += sBoxOutput[i];
                        break;
                    }
                }
            }


            return output;
        }

        public static String DecryptionSubstitution(String input)
        {
            var output = "";
            var sBoxInput = new[]
			{
				"0000", "0001", "0010", "0011",
				"0100", "0101", "0110", "0111",
				"1000", "1001", "1010", "1011",
				"1100", "1101", "1110", "1111"
			};

            var sBoxOutput = new[]
			{
				"1001", "0100", "1010", "1011",
				"1101", "0001", "1000", "0101",
				"0110", "0010", "0000", "0011",
				"1100", "1110", "1111", "0111"
			};

            for (var j = 0; j < input.Length / 4; j++)
            {
                var s = input.Substring(j * 4, 4);
                for (var i = 0; i < 16; i++)
                {
                    if (sBoxOutput[i] == s)
                    {
                        output += sBoxInput[i];
                        break;
                    }
                }
            }


            return output;
        }

        public static String[] GenerateKey(String input)
        {
            var w = new string[6];
            var k = new string[3];

            w[0] = input.Substring(0, 8);
            w[1] = input.Substring(8, 8);

            w[2] = XOR(w[0], XOR("10000000", EncryptionSubstitution(RotateTheNibble(w[1]))));

            w[3] = XOR(w[1], w[2]);

            w[4] = XOR(w[2], XOR("00110000", EncryptionSubstitution(RotateTheNibble(w[3]))));

            w[5] = XOR(w[4], w[3]);

            //Console.WriteLine(w[0]+"\n"+w[1]+"\n"+w[2]+"\n"+w[3]+"\n"+w[4]+"\n"+w[5]);

            for (int i = 0; i < 3; i++)
            {
                k[i] = w[2 * i] + w[2 * i + 1];
            }

            return k;
        }

        public static String RotateTheNibble(String input)
        {
            var s = input.Substring(input.Length / 2, input.Length / 2);
            s += input.Substring(0, input.Length / 2);

            return s;
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

        public static int Matrix(int row, int column)
        {
            int[,] matrix = new int[16, 16]
           {  
              {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0},
              {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15},
              {0,2,4,6,8,10,12,14,3,1,7,5,11,9,15,13},
              {0,3,6,5,12,15,10,9,11,8,13,14,7,4,1,2},
              {0,4,8,12,3,7,11,15,6,2,14,10,5,1,13,9},
              {0,5,10,15,7,2,13,8,14,11,4,1,9,12,3,6},
              {0,6,12,10,11,13,7,1,5,3,9,15,14,8,2,4},
              {0,7,14,9,15,8,1,6,13,10,3,4,2,5,12,11},
              {0,8,3,11,6,14,5,13,12,4,15,7,10,2,9,1},
              {0,9,1,8,2,11,3,10,4,13,5,12,6,15,7,14},
              {0,10,7,13,14,4,9,3,15,5,8,2,1,11,6,12},
              {0,11,5,14,10,1,15,4,7,12,2,9,13,6,8,3},
              {0,12,11,7,5,9,14,2,10,6,1,13,15,3,4,8},
              {0,13,9,4,1,12,8,5,2,15,11,6,3,14,10,7},
              {0,14,15,1,13,3,2,12,9,7,6,8,4,10,11,5},
              {0,15,13,2,9,6,4,11,1,14,12,3,8,7,5,10}
           }
             ;

            return matrix[row, column];


        }

        public static int GFAddition(int row, int column)
        {
            int[,] matrix = new int[16, 16]
           {  
              {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15},
              {1,0,3,2,5,4,7,6,9,8,11,10,13,12,15,14},
              {2,3,0,1,6,7,4,5,10,11,8,9,14,15,12,13},
              {3,2,1,0,7,6,5,4,11,10,9,8,15,14,13,12},
              {4,5,6,7,0,1,2,3,12,13,14,15,8,9,10,11},
              {5,4,7,6,1,0,3,2,13,12,15,14,9,8,11,10},
              {6,7,4,5,2,3,0,1,14,15,12,13,10,11,8,9},
              {7,6,5,4,3,2,1,0,15,14,13,12,11,10,9,8},
              {8,9,10,11,12,13,14,15,0,1,2,3,4,5,6,7},
              {9,8,11,10,13,12,15,14,1,0,3,2,5,4,7,6},
              {10,11,9,8,14,15,12,13,2,3,0,1,6,7,4,5},
              {11,10,9,8,15,14,13,12,3,2,1,0,7,6,5,4},
              {12,13,14,15,8,9,10,11,4,5,6,7,1,0,2,3},
              {13,12,15,14,9,8,11,10,5,4,7,6,0,1,3,2},
              {14,15,12,13,10,11,8,9,6,7,4,5,2,3,0,1},
              {15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0}
           }
            ;

            return matrix[row, column];
        }

        public static String MultiplicationForEncryption(String input)
        {
            int[] column = new int[4];
            //String[] row = new String[4];
            String output00, output01, output10, output11, output;

            for (int i = 0; i < input.Length / 4; i++)
            {
                column[i] = StringToInteger(input.Substring(i * 4, 4));

                //Console.WriteLine(column[i]);
            }

            output00 = XOR(integerToBinaryString(Matrix(4, column[2])), integerToBinaryString(column[0]));
            output01 = XOR(integerToBinaryString(Matrix(4, column[3])), integerToBinaryString(column[1]));
            output10 = XOR(integerToBinaryString(Matrix(4, column[0])), integerToBinaryString(column[2]));
            output11 = XOR(integerToBinaryString(Matrix(4, column[1])), integerToBinaryString(column[3]));

            //Console.WriteLine("\n\n"+output00+"\n"+output01+"\n"+output10+"\n"+output11);

            output = output00 + output10 + output01 + output11;

            return output;
        }

        public static String MultiplicationForDecryption(String input)
        {
            int[] column = new int[4];

            String output00, output01, output10, output11, output;

            for (int i = 0; i < input.Length / 4; i++)
            {
                column[i] = StringToInteger(input.Substring(i * 4, 4));

            }

            output00 = XOR(integerToBinaryString(Matrix(2, column[1])), integerToBinaryString(Matrix(9, column[0])));
            output01 = XOR(integerToBinaryString(Matrix(2, column[3])), integerToBinaryString(Matrix(9, column[2])));
            output10 = XOR(integerToBinaryString(Matrix(2, column[0])), integerToBinaryString(Matrix(9, column[1])));
            output11 = XOR(integerToBinaryString(Matrix(2, column[2])), integerToBinaryString(Matrix(9, column[3])));

            output = output00 + output10 + output01 + output11;

            return output;
        }

        public static int StringToInteger(String input)
        {

            int value = 0, remember, power = 0;
            int j = int.Parse(input);

            while (j > 0)
            {
                remember = j % 10;
                value = value + (int)(remember * Math.Pow(2, power));
                power++;
                j = (int)j / 10;
            }

            return value;
        }

        public static String integerToBinaryString(int num)
        {
            String remember = "";
            String finalString = "";

            while (num >= 1)
            {
                remember += (num % 2).ToString();
                num = num / 2;
            }

            for (int i = 0; i < 4 - remember.Length; i++)
            {
                finalString += 0;
            }

            for (int i = remember.Length - 1; i >= 0; i--)
            {
                finalString += remember[i];
            }

            return finalString;
        }
    }
}
