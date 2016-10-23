using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    class SBox
    {
        String final32BitString = "";
        public SBox(String main)
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










        



    }


}
