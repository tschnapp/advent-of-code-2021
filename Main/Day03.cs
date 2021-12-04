using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day03 : Day
    {
        [TestCase("Test01", "198")]
        [TestCase("Final", "2954600")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            int[] bitCounts = new int[input[0].Count()];
            var gammaRate = "";
            var epsilonRate = "";

            for (int b = 0; b < input.Count(); b++)                             // total zero bits
                for (int c = 0; c < input[b].Count(); c++)
                    if (input[b][c] == '0')
                        bitCounts[c]++;

            for (int c = 0; c < bitCounts.Count(); c++)                         // create gamma rate from totals
                gammaRate += bitCounts[c] > (input.Count() / 2) ? "0" : "1";

            foreach (char c in gammaRate)                                       // create epsilonRate rate from gamma rate
                epsilonRate += c == '0' ? "1" : "0";

            var result = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
            return result.ToString();
        }

        [TestCase("Test01", "230")]
        [TestCase("Final", "1662846")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            int zeroBitCountForColumn = 0;
            var oxygenGeneratorRating = "";
            var co2ScrubberRating = "";

            for (int c = 0; c < input[0].Count(); c++)          // Get oxygenGeneratorRating value
            {
                zeroBitCountForColumn = 0;
                for (int i = 0; i < input.Count(); i++)             // Get the bit counts for the zero values
                    if (input[i][c] == '0')
                        zeroBitCountForColumn++;

                var initialInputCount = input.Count();
                for (int b = initialInputCount - 1; b >= 0; b--)    // The for loop is reversed to allow removing items
                {
                    if ((zeroBitCountForColumn > initialInputCount / 2 && input[b][c] == '1') ||
                        (zeroBitCountForColumn <= initialInputCount / 2 && input[b][c] == '0'))
                    {
                        input.Remove(input[b]);                     // remove binary from the list when it doesn't match

                        if (input.Count() == 1)
                            break;
                    }
                }

                if (input.Count() == 1)
                    break;
            }

            oxygenGeneratorRating = input[0];


            input = rawInput.ToList();

            for (int c = 0; c < input[0].Count(); c++)          // Get co2ScrubberRating value
            {
                zeroBitCountForColumn = 0;
                for (int i = 0; i < input.Count(); i++)             // Get the bit counts for the zero values
                    if (input[i][c] == '0')
                        zeroBitCountForColumn++;

                var initialInputCount = input.Count();
                for (int b = initialInputCount - 1; b >= 0; b--)    // The for loop is reversed to allow removing items
                {
                    if ((zeroBitCountForColumn > initialInputCount / 2 && input[b][c] == '0') ||
                        (zeroBitCountForColumn <= initialInputCount / 2 && input[b][c] == '1'))
                    {
                        input.Remove(input[b]);                     // remove binary from the list when it doesn't match

                        if (input.Count() == 1)
                            break;
                    }
                }

                if (input.Count() == 1)
                    break;
            }

            co2ScrubberRating = input[0];

            var result = Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(co2ScrubberRating, 2);
            return result.ToString();
        }
    }
}