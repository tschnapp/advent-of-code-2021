using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day02 : Day
    {
        [TestCase("Test01", "150")]
        [TestCase("Final", "1882980")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var totalDistance = 0;
            var depth = 0;

            foreach (string command in input)
            {
                var inputSplit = command.Split(' ');
                var direction = inputSplit[0];
                var distance = Int32.Parse(inputSplit[1]);

                if (direction == "forward")
                    totalDistance += distance; 
                else if (direction == "down")
                    depth += distance;
                else if (direction == "up")
                    depth -= distance;
            }

            return (totalDistance * depth).ToString();
        }

        [TestCase("Test01", "900")]
        [TestCase("Final", "1971232560")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var totalDistance = 0;
            var depth = 0;
            var aim = 0;

            foreach (string command in input)
            {
                var inputSplit = command.Split(' ');
                var direction = inputSplit[0];
                var distance = Int32.Parse(inputSplit[1]);

                if (direction == "forward")
                {
                    totalDistance += distance;
                    depth += aim * distance;
                }
                else if (direction == "down")
                    aim += distance;
                else if (direction == "up")
                    aim -= distance;
            }

            return (totalDistance * depth).ToString();
        }
    }
}