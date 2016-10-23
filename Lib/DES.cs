using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Lib
{
    public class DES
    {
        public string Encrypt(string TplainText, string key)
        {

            if (key.Length != 8) return "Given Key Is Not Valid";

            var Idiots = "";

            for (int q = 0; q < (int)TplainText.Length / 8 + 1; q++)
            {
                var plainText = "";

                if (q < TplainText.Length / 8) plainText = TplainText.Substring(q * 8, 8);

                else plainText = TplainText.Substring(q * 8, TplainText.Length - q * 8);

                var M = "";

                if (plainText.Length == 0) break;

                else if (plainText.Length < 8)
                {
                    for (int w = plainText.Length; w < 8; w++)
                    {
                        M += "00000000";
                    }
                    M += StringToBitString(plainText);
                }

                else
                {
                    M = StringToBitString(plainText);
                }

                var K = new String[17];

                var KeyOfFourtyEigntBit = new String[16];


                var IP = GenerateIP(M);

                String L0 = IP.Substring(0, 32), R0 = IP.Substring(32, 32);

                K[0] = generateKeyOf56Bit(key);

                String L1 = "", R1 = "";


                for (int i = 0; i < 16; i++)
                {
                    L1 = R0;

                    var expansioned48BitString = "";
                    expansioned48BitString = expansionPermutation(R0);

                    K[i + 1] = ShiftKey(K[i], KeyShiftAmount(i));

                    var inputForSBox = "";
                    KeyOfFourtyEigntBit[i] = PC2Permutation(K[i + 1]);


                    inputForSBox = XOR(expansioned48BitString, KeyOfFourtyEigntBit[i]);

                    var sBox32BitOutputString = "";
                    SBox(inputForSBox);
                    sBox32BitOutputString = getOutput();

                    var pBoxPermutation32BitOutput = "";
                    pBoxPermutation32BitOutput = pBox(sBox32BitOutputString);



                    R1 = XOR(L0, pBoxPermutation32BitOutput);


                    R0 = R1;
                    L0 = L1;

                }

                var BitStringOfCipherText = R0 + L0;

                BitStringOfCipherText = GenerateIP_1(BitStringOfCipherText);

                Idiots += BitStringOfCipherText;
            }

            return Idiots;
        }


        public string Decrypt(string TcipherText, string k)
        {

            if (k.Length != 8) return "Given Key Is Not Valid";


            var Idiots = "";

            for (int q = 0; q < TcipherText.Length / 8 + 1; q++)
            {
                var cipherText = "";
                if (q < TcipherText.Length / 8) cipherText = TcipherText.Substring(q * 8, 8);

                else cipherText = TcipherText.Substring(q * 8, TcipherText.Length - q * 8);

                
                var M = "";

                if (cipherText.Length == 0) break;

                else if (cipherText.Length < 8)
                {
                    for (int w = cipherText.Length; w < 8; w++)
                    {
                        M += "00000000";
                    }
                    M += StringToBitString(cipherText);
                }

                else
                {
                    M = StringToBitString(cipherText);
                }

                var IP = GenerateIP(M);




                var K = new String[17];

                K[0] = generateKeyOf56Bit(k);

                for (int h = 0; h < 16; h++)
                {
                    K[h + 1] = ShiftKey(K[h], KeyShiftAmount(h));
                }


                String L0 = IP.Substring(0, 32), R0 = IP.Substring(32, 32);

                String L0R0 = L0 + R0;
                String L1 = "", R1 = "";

                for (var i = 0; i < 16; i++)
                {
                    L1 = R0;

                    var expansioned48BitString = expansionPermutation(R0);

                    var inputForSBox = XOR(expansioned48BitString, PC2Permutation(K[16 - i]));

                    SBox(inputForSBox);

                    var sBox32BitOutputString = getOutput();

                    var pBoxPermutation32BitOutput = pBox(sBox32BitOutputString);

                    R1 = XOR(L0, pBoxPermutation32BitOutput);

                    R0 = R1;
                    L0 = L1;


                }

                var BitStringForPlainText = R0 + L0;
                BitStringForPlainText = GenerateIP_1(BitStringForPlainText);



                if (q == TcipherText.Length / 8 - 1)
                {
                    var temp = "";
                    for (int i = 0; i < 8; i++)
                    {
                        if (StringToInteger(BitStringForPlainText.Substring(i * 8, 8)) != 0)
                        {
                            temp += BitStringForPlainText.Substring(i * 8, 8);
                        }
                    }

                    BitStringForPlainText = temp;
                }

                Idiots += BitStringForPlainText;
            }



            return Idiots;
        }


        List<int> baseArrayForIp = new List<int>
        {
            58, 50	,42,34,	26,	18,	10,	2,
            60,	52,	44,	36,	28,	20,	12,	4,
            62,	54,	46	,38, 30,22,	14,	6,
            64,	56,	48,	40,	32,	24,	16,	8,
            57,	49,	41,	33,	25,	17,	9,	1,
            59,	51,	43,	35,	27,	19,	11,	3,
            61,	53,	45,	37,	29,	21,	13,	5,
            63,	55,	47,	39,	31,	23,	15,	7

        };

        List<int> baseArrayForIp1 = new List<int>
        {
            40,	8,	48,	16,	56,	24,	64,	32,
            39,	7,	47,	15,	55,	23,	63,	31,
            38,	6,	46,	14,	54,	22,	62,	30,
            37,	5,	45,	13,	53,	21,	61,	29,
            36,	4,	44,	12,	52,	20,	60,	28,
            35,	3,	43,	11,	51,	19,	59,	27,
            34,	2,	42,	10,	50,	18,	58,	26,
            33,	1,	41,	9,	49,	17,	57,	25
        };


        public string GenerateIP(string initialString)
        {

            var ipString = "";
            foreach (int i in baseArrayForIp)
            {
                ipString = ipString + initialString[i - 1];
            }

            return ipString;
        }


        public string pBox(String input)
        {
            List<int> pBoxFormat = new List<int>{
                16,  7, 20, 21,
                29, 12, 28, 17,
                1, 15, 23, 26,
                5, 18, 31, 10,
                2,  8, 24, 14,
                32, 27,  3,  9,
                19, 13, 30,  6,
                22, 11,  4, 25                 
            };

            var str = input;


            var formattedString = "";

            foreach (var i in pBoxFormat)
            {
                formattedString = formattedString + str[i - 1];
            }

            return formattedString;
        }


        public string GenerateIP_1(string initialString)
        {
            var ipString = "";
            foreach (int i in baseArrayForIp1)
            {
                ipString = ipString + initialString[i - 1];
            }
            return ipString;
        }


        String final32BitString = "";


        public void SBox(String main)
        {
            String mainString = main;

            int[] rowForSBoxArgument = new int[8];

            int[] columnForSBoxArgument = new int[8];

            int[] SBoxInteger = new int[8];

            for (int SBoxNumber = 0; SBoxNumber < 8; SBoxNumber++)
            {
                String temporary6BitStringOfInputString = "";

                for (int i = SBoxNumber * 6; i < (SBoxNumber + 1) * 6; i++)
                {
                    temporary6BitStringOfInputString += mainString[i];
                }

                String firstAndLast2BitOf6BitString = "";

                firstAndLast2BitOf6BitString += temporary6BitStringOfInputString[0];

                firstAndLast2BitOf6BitString += temporary6BitStringOfInputString[5];

                String Middle4BitOf6BitString = "";

                for (int l = 1; l < 5; l++)
                {
                    Middle4BitOf6BitString += temporary6BitStringOfInputString[l];
                }

                rowForSBoxArgument[SBoxNumber] = StringToInteger(firstAndLast2BitOf6BitString);

                columnForSBoxArgument[SBoxNumber] = StringToInteger(Middle4BitOf6BitString);


            }

            SBoxInteger[0] = SBoxFirst(rowForSBoxArgument[0], columnForSBoxArgument[0]);
            SBoxInteger[1] = SBoxSecond(rowForSBoxArgument[1], columnForSBoxArgument[1]);
            SBoxInteger[2] = SBoxThird(rowForSBoxArgument[2], columnForSBoxArgument[2]);
            SBoxInteger[3] = SBoxFourth(rowForSBoxArgument[3], columnForSBoxArgument[3]);
            SBoxInteger[4] = SBoxFifth(rowForSBoxArgument[4], columnForSBoxArgument[4]);
            SBoxInteger[5] = SBoxSixth(rowForSBoxArgument[5], columnForSBoxArgument[5]);
            SBoxInteger[6] = SBoxSeventh(rowForSBoxArgument[6],columnForSBoxArgument[6]);
            SBoxInteger[7] = SBoxEighth(rowForSBoxArgument[7], columnForSBoxArgument[7]);

            final32BitString = "";

            for (int i = 0; i < SBoxInteger.Length; i++)
            {
                final32BitString += integerToBinaryString(SBoxInteger[i]);
            }

        }


        public String getOutput()
        {
            return final32BitString;
        }


        public int StringToInteger(String input)
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


        public String integerToBinaryString(int num)
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


        public int SBoxFirst(int row, int column)
        {
            int[,] SBoxOne = new int[4, 16] {	{ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 }, 
                { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 }, 
                { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 }, 
                { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } 
            };

            return SBoxOne[row, column];
        }


        public int SBoxSecond(int row, int column)
        {
            int[,] SBoxTwo = new int[4, 16] {	{ 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 }, 
                { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 }, 
                { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 }, 
                {13, 8 ,10, 1,  3, 15, 4, 2, 11, 6, 7, 12,  0, 5, 14, 9} 
            };

            return SBoxTwo[row, column];
        }


        public int SBoxThird(int row, int column)
        {
            int[,] SBoxThree = new int[4, 16] {	{ 10, 0,  9, 14,  6, 3, 15,  5,  1, 13, 12, 7, 11,  4,  2, 8}, 
                { 13, 7,  0,  9,  3,  4, 6, 10, 2, 8, 5, 14, 12, 11, 15,  1 }, 
                { 13,  6, 4, 9, 8, 15,  3,  0,  11,  1, 2, 12,  5, 10, 14, 7}, 
                { 1, 10, 13, 0,  6, 9, 8,  7,  4, 15,  14, 3,  11, 5,  2, 12} 
            };

            return SBoxThree[row, column];
        }


        public int SBoxFourth(int row, int column)
        {
            int[,] SBoxFour = new int[4, 16] {	{ 7, 13,  14,  3,   0,  6,   9, 10,   1,  2,   8,  5,  11, 12,   4, 15}, 
                { 13,  8,  11,  5,   6, 15,  0,  3,   4,  7,   2, 12,   1, 10,  14,  9},
                { 10,  6,  9,  0,  12, 11,   7, 13,  15,  1,   3, 14,   5,  2,   8,  4},
                { 3, 15,   0,  6,  10,  1,  13,  8,   9,  4,   5, 11,  12,  7,   2, 14}
            };

            return SBoxFour[row, column];
        }


        public int SBoxFifth(int row, int column)
        {
            int[,] SBoxFive = new int[4, 16] {	{ 2, 12,   4,  1,   7, 10,  11,  6,   8,  5,   3, 15,  13,  0,  14,  9}, 
                { 14, 11,   2, 12,  4,  7,  13,  1,   5,  0,  15, 10,   3,  9,   8,  6},
                { 4,  2,   1, 11,  10, 13,   7,  8,  15,  9,  12,  5,   6,  3,   0, 14}, 
                { 11, 8,  12,  7,   1, 14,   2, 13,   6, 15,   0,  9,  10,  4,   5,  3}
            };

            return SBoxFive[row, column];
        }


        public int SBoxSixth(int row, int column)
        {
            int[,] SBoxSix = new int[4, 16] {	{ 12,  1,  10, 15,   9,  2,   6,  8,   0, 13,   3,  4,  14,  7,   5, 11},
                { 10, 15,   4,  2,   7, 12,   9,  5,   6,  1,  13, 14,   0, 11,   3,  8},
                { 9, 14,  15,  5,   2,   8,  12,  3,   7,  0,   4, 10,   1, 13,  11,  6},
                { 4,  3,   2, 12,   9,  5,  15, 10,  11, 14,   1,  7,    6,  0,   8, 13}
            };

            return SBoxSix[row, column];
        }


        public int SBoxSeventh(int row, int column)
        {
            int[,] SBoxSeven = new int[4, 16] {	{ 4, 11,   2, 14,  15,  0,   8, 13,   3, 12,   9,  7,   5, 10,   6,  1}, 
                { 13,  0,  11,  7,  4,  9,   1, 10,  14,  3,   5, 12,   2, 15,   8,  6}, 
                { 1,  4,  11,  13,  12,  3,   7,  14,  10, 15,  6,  8,   0,  5,  9,  2}, 
                { 6, 11,  13,  8,   1,  4,  10,  7,   9,  5,   0, 15,  14,  2,   3, 12} 
            };

            return SBoxSeven[row, column];
        }


        public int SBoxEighth(int row, int column)
        {
            int[,] SBoxEight = new int[4, 16] {	{ 13,  2,   8,  4,   6, 15,  11,  1,  10,  9,   3, 14,   5,  0,  12,  7}, 
                { 1, 15,  13,  8,   10,  3,   7,  4,  12,  5,   6, 11,   0, 14,   9,  2}, 
                { 7, 11,   4,  1,   9, 12,  14,   2,   0,  6,  10, 13,  15,  3,   5,  8}, 
                { 2,  1,  14,  7,   4, 10,   8, 13,  15, 12,   9,  0,   3,  5,   6,  11} 
            };

            return SBoxEight[row, column];
        }


        public String XOR(String s1, String s2)
        {
            String s = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == s2[i])
                {
                    s += '0';
                }
                else
                {
                    s += '1';
                }
            }

            return s;
        }


        public String StringToBitString(String inputString)
        {

            String BitString = "";
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

            return BitString;

        }


        public String BitStringToString(String input)
        {

            String OutputString = "";

            for (int i = 0; i < input.Length / 8; i++)
            {
                String TemporaryToSave8BitOfASingleCharecter = "";

                for (int j = i * 8; j < (i + 1) * 8; j++)
                {
                    TemporaryToSave8BitOfASingleCharecter += input[j];
                }

                int ascii = 0;

                for (int p = 0; p < 8; p++)
                {
                    int v = (int)Char.GetNumericValue(TemporaryToSave8BitOfASingleCharecter[p]);
                    ascii += v * (int)Math.Pow(2, 7 - p);
                }

                OutputString += Convert.ToChar(ascii);

            }

            return OutputString;
        }


        public String PC2Permutation(String key)
        {
            int[] pc2 = new int[] {  14 ,   17   ,11,    24   ,  1,    5,
							  3    ,28   ,15  ,   6    ,21  , 10,
							 23    ,19  , 12   ,  4    ,26  ,  8,
							 16     ,7  , 27    ,20  ,  13   , 2,
							 41  ,  52  , 31    ,37,    47   ,55,
							 30   , 40   ,51    ,45    ,33   ,48,
							 44    ,49  , 39    ,56  ,  34   ,53,
							 46   , 42 ,  50    ,36 ,   29  , 32 };

            String permuted48BitKey = "";
            foreach (int i in pc2)
            {
                permuted48BitKey += key[i - 1];
            }

            return permuted48BitKey;
        }


        public String generateKeyOf56Bit(string key)
        {
            String Key = "";
            

            key = StringToBitString(key);

            int[] pc1 = new int[] { 57, 49, 41, 33, 25, 17,  9,
						       1,   58    ,50  , 42   , 34   , 26  , 18,
						      10,    2   , 59   ,51,    43    ,35,   27,
						      19 ,  11    , 3   ,60  ,  52    ,44  , 36,
						      63  , 55    ,47  , 39 ,   31    ,23   ,15,
						       7   ,62   , 54  , 46   , 38    ,30   ,22,
						      14    ,6    ,61  , 53   , 45    ,37   ,29,
						      21   ,13     ,5 ,  28    ,20   , 12  ,  4};

            foreach (int i in pc1)
            {
                Key += key[i - 1];
            }

            return Key;
        }


        public String ShiftKey(String key, int shift)
        {
            String C1 = "", D1 = "";
            String C = "", D = "";

            C = key.Substring(0, 28);
            D = key.Substring(28, 28);

            for (int i = shift; i < C.Length; i++)
            {
                C1 += C[i];
                D1 += D[i];
            }

            for (int i = 0; i < shift; i++)
            {
                C1 += C[i];
                D1 += D[i];
            }


            String K = C1 + D1;

            return K;
        }


        public int KeyShiftAmount(int i)
        {
            int[] a = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            return a[i];
        }


        public int KeyShiftAmountDescryption(int i)
        {
            int[] a = new int[] { 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 1};
            return a[i];
        }


        public String RightShiftKey(String key, int shift)
        {
            String C1 = "", D1 = "";
            String C = "", D = "";

            C = key.Substring(0, 28);
            D = key.Substring(28, 28);

            for (int i = C.Length - shift; i < C.Length; i++)
            {
                C1 += C[i];
                D1 += D[i];
            }

            for (int i = 0; i < C.Length-shift; i++)
            {
                C1 += C[i];
                D1 += D[i];
            }



            String K = C1 + D1;

            return K;
        }

        
        public String expansionPermutation(String input)
        {
            int[] a = new int[] { 32 ,	1 ,	2, 	3, 	4, 	5,
							4 ,	5 ,	6 ,	7 ,	8 ,	9,
							8 ,	9,     10 ,	11 ,	12 ,	13,
							12 ,	13 ,	14 ,	15 ,	16 ,	17,
							16 ,	17 ,	18 ,	19 ,	20 ,	21,
							20 ,	21 ,	22 ,	23 ,	24 ,	25,
							24 ,	25 ,	26 ,	27, 	28 ,	29,
							28 ,	29, 	30 ,	31 ,	32 ,	1 };
            String ExpandedString = "";

            foreach (int i in a)
            {
                ExpandedString += input[i - 1];
            }

            return ExpandedString;
        }

    }
}