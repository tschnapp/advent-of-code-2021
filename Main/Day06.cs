using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day06 : Day
    {
        [TestCase("Test01", "5934")]
        [TestCase("Final", "372300")]
        public override string Part01(string[] rawInput)
        {
            var fishes = rawInput[0].Split(",").Select(Int32.Parse).ToList();

            for (int d = 0; d < 80; d++)
            {
                for (int f = 0; f < fishes.Count(); f++)
                {
                    fishes[f]--;
                    if (fishes[f] < 0)
                    {
                        fishes[f] = 6;
                        fishes.Add(9);
                    }
                }
            }

            return fishes.Count().ToString();
        }

        [TestCase("Test01", "26984457539")]
        [TestCase("Final", "1675781200288")]
        public override string Part02(string[] rawInput)
        {
            var fishes = rawInput[0].Split(",").Select(Int32.Parse).ToList();
            long totalFish = fishes.Count();
            long[] spawnDays = new long[300];

            for (int f = 0; f < fishes.Count(); f++)    // Add initial fish to spawnDays schedule
                spawnDays[fishes[f]]++;

            for (int d = 0; d < 256; d++) 
            {
                spawnDays[d + 7] += spawnDays[d];       // Reschedule existing fish on spawnDays
                spawnDays[d + 9] += spawnDays[d];       // Schedule offspring on spawnDays
                totalFish += spawnDays[d];
            }

            return totalFish.ToString();
        }
    }
}