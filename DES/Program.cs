using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {

            var program = new Program();
            var permutation = new Permutation();


            Console.WriteLine(program.BitStringToString("0000000011111111100000000110011000000000111111110111100001010101"));

            var K = new String[17];

            var KeyOfFourtyEigntBit = new String[16];

            //var M = "0000000100100011010001010110011110001001101010111100110111101111";
            var M = program.StringToBitString("12345678");

            Console.WriteLine("Given Plain Text \t\t\t" + M);

            var IP = permutation.GenerateIP(M);


            String L0 = IP.Substring(0, 32),R0 = IP.Substring(32, 32);

            K[0] = program.generateKeyOf56Bit();

            //Console.WriteLine("First Key "+K[0]);

            String L1 = "", R1 = "";


            for (int i = 0; i < 16; i++)
            {
                L1 = R0;

                var expansioned48BitString = "";
                expansioned48BitString = program.expansionPermutation(R0);
                
                K[i + 1] = program.ShiftKey(K[i], program.KeyShiftAmount(i));

                var inputForSBox = "";
                KeyOfFourtyEigntBit[i] = program.PC2Permutation(K[i + 1]);


                inputForSBox = program.XOR(expansioned48BitString, KeyOfFourtyEigntBit[i]);

                var sBox32BitOutputString = "";
                var s = new SBox(inputForSBox);
                sBox32BitOutputString = s.getOutput();

                var pBoxPermutation32BitOutput = "";
                pBoxPermutation32BitOutput = permutation.pBox(sBox32BitOutputString);

                

                R1 = program.XOR(L0, pBoxPermutation32BitOutput);


                R0 = R1;
                L0 = L1;

            }

            var BitStringOfCipherText = R0 + L0;

            BitStringOfCipherText = permutation.GenerateIP_1(BitStringOfCipherText);

            Console.WriteLine("\n\nCipher Text For Given Plain Text : \t" + BitStringOfCipherText);

            //var CipherText = program.BitStringToString(BitStringOfCipherText);

            //Console.WriteLine("\nThe Cipher Text : " + BitStringOfCipherText + "\n\n");

            Decryption(BitStringOfCipherText,KeyOfFourtyEigntBit);
            Console.WriteLine();

            Console.Read();
        }

	    public static void Decryption(String bitStringForCipherText, String[] key)
        {

            var pmt = new Permutation();
            var p = new Program();

	        var BitStringForCipherText = pmt.GenerateIP(bitStringForCipherText);

		    String L0 = BitStringForCipherText.Substring(0, 32);
		    String R0 = BitStringForCipherText.Substring(32, 32);

	        String L1  = "", R1="";

            for (var i = 0; i < 16; i++)
            {
                L1 = R0;

                var expansioned48BitString = p.expansionPermutation(R0);

                var inputForSBox =  p.XOR(expansioned48BitString, key[15-i]);

		        var s = new SBox(inputForSBox);

                var sBox32BitOutputString = s.getOutput();

                var pBoxPermutation32BitOutput = pmt.pBox(sBox32BitOutputString);

                R1 = p.XOR(L0, pBoxPermutation32BitOutput);

                R0 = R1;
                L0 = L1;
            }

            var BitStringForPlainText = R0 + L0;
	        BitStringForPlainText = pmt.GenerateIP_1(BitStringForPlainText);


	        Console.WriteLine("\n\nPlain Text For Given Cipher Text Bit : \t"+BitStringForPlainText);

            Console.Read();
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

        public String generateKeyOf56Bit()
        {
		String Key = "";
            //var key = "0001001100110100010101110111100110011011101111001101111111110001";
        var key = StringToBitString("12345678");

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
