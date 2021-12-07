using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day07 : Day
    {
        [TestCase("Test01", "37")]
        [TestCase("Final", "353800")]
        public override string Part01(string[] rawInput)
        {
            var crabSubLoc = rawInput[0].Split(",").Select(Int32.Parse).ToList();
            var minCrabSubLoc = crabSubLoc.Min(x => x);
            var maxCrabSubLoc = crabSubLoc.Max(x => x);
            var lowestFuelCount = maxCrabSubLoc * crabSubLoc.Count();

            for (int x = minCrabSubLoc; x <= maxCrabSubLoc; x++)
            {
                var fuelCount = 0;
                for (int i = 0; i < crabSubLoc.Count(); i++)
                    fuelCount += Math.Abs(crabSubLoc[i] - x);

                lowestFuelCount = fuelCount < lowestFuelCount ? fuelCount : lowestFuelCount ;
            }

            return lowestFuelCount.ToString();
        }

        [TestCase("Test01", "168")]
        [TestCase("Final", "98119739")]
        public override string Part02(string[] rawInput)
        {
            var crabSubLoc = rawInput[0].Split(",").Select(Int32.Parse).ToList();
            var minCrabSubLoc = crabSubLoc.Min(x => x);
            var maxCrabSubLoc = crabSubLoc.Max(x => x);
            var lowestFuelCount = Enumerable.Range(0, maxCrabSubLoc + 1).Sum() * crabSubLoc.Count();

            for (int x = minCrabSubLoc; x <= maxCrabSubLoc; x++)
            {
                var fuelCount = 0;
                for (int i = 0; i < crabSubLoc.Count(); i++)
                {
                    var distance = Math.Abs(crabSubLoc[i] - x);
                    fuelCount += Enumerable.Range(0, distance + 1).Sum();
                }

                lowestFuelCount = fuelCount < lowestFuelCount ? fuelCount : lowestFuelCount;
            }

            return lowestFuelCount.ToString();
        }
    }
}