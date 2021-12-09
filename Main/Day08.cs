using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day08 : Day
    {
        [TestCase("Test01", "26")]
        [TestCase("Final", "534")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var segmentsCount = 0;

            foreach (string inputLine in input)
            {    
                var segments = inputLine.Split(" | ")[1].Split(" ");

                foreach(string segment in segments)
                    if ((segment.Count() >= 2 && segment.Count() <= 4) || segment.Count() == 7)
                        segmentsCount++;
            }

            return segmentsCount.ToString();
        }

        [TestCase("Test01", "61229")]
        [TestCase("Final", "1070188")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var segmentValues = "0000000".ToCharArray();
            var numbersTotal = 0;

            // Set the known 1, 4, 7, and 8 numbers. Identify the unknown 5 and 6 segment numbers.
            foreach (string inputLine in input)
            {
                var numbers = inputLine.Split(" | ")[0].Split(" ");
                var sets = new List<string>() { "", "", "", "", "", "", "", "", "", "" };
                var hasFiveChars = new List<string>();
                var hasSixChars = new List<string>();

                for (int n = 0; n < numbers.Count(); n++)
                {
                    switch (numbers[n].Count())
                    {
                        case 2:     // map 1 
                            sets[1] = String.Concat(numbers[n].OrderBy(c => c));
                            break;
                        case 3:     // map 7
                            sets[7] = String.Concat(numbers[n].OrderBy(c => c));
                            break;
                        case 4:     // map 4
                            sets[4] = String.Concat(numbers[n].OrderBy(c => c));
                            break;
                        case 7:     // map 8
                            sets[8] = String.Concat(numbers[n].OrderBy(c => c));
                            break;
                        case 5:     // have 5 chars
                            hasFiveChars.Add(numbers[n]);
                            break;
                        case 6:     // have 6 chars
                            hasSixChars.Add(numbers[n]);
                            break;
                    }
                }

                // Deduce the segments based on the ones what we know.
                segmentValues[0] = RemoveAFromB(sets[1], sets[7])[0];
                segmentValues[3] = CommonCharsInAll(new List<string>() { sets[4], CommonCharsInAll(new List<string>() { hasFiveChars[0], hasFiveChars[1], hasFiveChars[2] }) })[0];
                segmentValues[6] = RemoveAFromB(string.Join("", segmentValues[0], segmentValues[3]), CommonCharsInAll(new List<string>() { hasFiveChars[0], hasFiveChars[1], hasFiveChars[2] }))[0];
                segmentValues[1] = RemoveAFromB(segmentValues[3].ToString(), RemoveAFromB(sets[1], sets[4]))[0];
                segmentValues[2] = RemoveAFromB(CommonCharsInAll(new List<string>() { hasSixChars[0], hasSixChars[1], hasSixChars[2] }), sets[1])[0];
                segmentValues[5] = RemoveAFromB(segmentValues[2].ToString(), sets[1])[0];
                segmentValues[4] = RemoveAFromB(string.Join("", segmentValues[0], segmentValues[1], segmentValues[2], segmentValues[3], segmentValues[5], segmentValues[6]), "abcdefg")[0];


                // Build the number strings from the segments.
                foreach (string segment in numbers)
                {
                    sets[0] = String.Concat(string.Join("", segmentValues[0], segmentValues[1], segmentValues[2], segmentValues[4], segmentValues[5], segmentValues[6]).OrderBy(c => c));
                    sets[2] = String.Concat(string.Join("", segmentValues[0], segmentValues[2], segmentValues[3], segmentValues[4], segmentValues[6]).OrderBy(c => c));
                    sets[3] = String.Concat(string.Join("", segmentValues[0], segmentValues[2], segmentValues[3], segmentValues[5], segmentValues[6]).OrderBy(c => c));
                    sets[5] = String.Concat(string.Join("", segmentValues[0], segmentValues[1], segmentValues[3], segmentValues[5], segmentValues[6]).OrderBy(c => c));
                    sets[6] = String.Concat(string.Join("", segmentValues[0], segmentValues[1], segmentValues[3], segmentValues[4], segmentValues[5], segmentValues[6]).OrderBy(c => c));
                    sets[9] = String.Concat(string.Join("", segmentValues[0], segmentValues[1], segmentValues[2], segmentValues[3], segmentValues[5], segmentValues[6]).OrderBy(c => c));
                }

                // Convert the digits to numbers and add to a total.
                var digits = inputLine.Split(" | ")[1].Split(" ");
                var number = "";

                foreach(string d in digits)
                    for (int s = 0; s < 10; s++)
                        if (String.Concat(d.OrderBy(c => c)) == sets[s])
                            number += s.ToString();

                numbersTotal += Int32.Parse(number);
            }

            return numbersTotal.ToString();
        }

        private string RemoveAFromB(string a, string b)
        {
            return new string(b.Where(c => !a.Contains(c)).ToArray());
        }

        private string CommonCharsInAll(List<string> numbers)
        {
            var commonChars = "";
            var allChars = String.Join("", numbers);
            string distinctChars = new String(allChars.Distinct().ToArray());

            foreach (char ch in distinctChars)
                commonChars += allChars.Count(x => x == ch) == numbers.Count() ? ch.ToString() : "";

            return commonChars;
        }

    }
}