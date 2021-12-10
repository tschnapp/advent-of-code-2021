using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day10 : Day
    {
        [TestCase("Test01", "26397")]
        [TestCase("Final", "167379")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var stack = new List<string>();
            var openingChars = "([{<";
            var closingChars = ")]}>";
            var total = 0;

            foreach (string error in input)
            {
                var subTotal = 0;

                foreach (char c in error)
                {
                    if (openingChars.Contains(c.ToString()))                                        // opening char
                    {
                        stack.Add(c.ToString());
                    }
                    else if (closingChars.Contains(c.ToString()))                                   // closing char
                    {
                        if (openingChars.IndexOf(stack.Last()) != closingChars.IndexOf(c))          // bad char
                        {
                            subTotal += new int[] { 3, 57, 1197, 25137 }[closingChars.IndexOf(c)];  // add to total
                            break;
                        }

                        stack.RemoveAt(stack.Count() - 1);
                    }
                }

                total += subTotal;
            }

            return total.ToString();
        }

        [TestCase("Test01", "288957")]
        [TestCase("Final", "2776842859")] // too low
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var openingChars = "([{<";
            var closingChars = ")]}>";
            var totals = new List<long>();

            foreach (string error in input)
            {
                var stack = new List<string>();
                var isCorrupt = false;
                long subTotal = 0;

                foreach (char c in error)
                {
                    if (openingChars.Contains(c.ToString()))                                        // opening char
                    {
                        stack.Add(c.ToString());
                    }
                    else if (closingChars.Contains(c.ToString()))                                   // closing char
                    {
                        if (openingChars.IndexOf(stack.Last()) != closingChars.IndexOf(c))          // bad char, skip this one
                        {
                            isCorrupt = true;
                            break;
                        }

                        stack.RemoveAt(stack.Count() - 1);
                    }
                }

                if (!isCorrupt)
                {
                    for (int c = stack.Count() - 1; c >= 0; c--)
                    {
                        subTotal = (subTotal * 5) + openingChars.IndexOf(stack[c]) + 1;             // get sub total
                    }

                    totals.Add(subTotal);                                                           // add sub total to total list 
                }
            }

            return totals.OrderBy(x => x).ToList()[totals.Count() / 2].ToString();                  // return middle total
        }
    }
}