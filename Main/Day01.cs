using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day01 : Day
    {
        [TestCase("Test01", "7")]
        [TestCase("Final", "1752")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.Select(Int32.Parse).ToList();
            var largerMeasurementsCount = 0;

            for (int i = 1; i < input.Count; i++)
                if (input[i] > input[i - 1])
                    largerMeasurementsCount++;

            return largerMeasurementsCount.ToString();
        }

        [TestCase("Test01", "5")]
        [TestCase("Final", "1781")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.Select(Int32.Parse).ToList();

            var largerMeasurementsCount = 0;
            for (int i = 3; i < input.Count; i++)
                if (input.GetRange(i - 2, 3).Sum() > input.GetRange(i - 3, 3).Sum())
                    largerMeasurementsCount++;

            return largerMeasurementsCount.ToString();
        }
    }
}