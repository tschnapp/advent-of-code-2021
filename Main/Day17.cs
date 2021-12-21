using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day17 : Day
    {
        //[TestCase("Test01", "45")]
        //[TestCase("Final", "4851")]
        public override string Part01(string[] rawInput)
        {
            //target area: x=20..30, y=-10..-5      // test
            //target area: x=201..230, y=-99..-65   // final

            var input = rawInput.ToList();          // skipped the parsing and just directly input the values

            //var xPos1 = 20;                         // test data
            //var xPos2 = 30;
            //var yPos1 = -5;
            //var yPos2 = -10;

            var xPos1 = 201;                        // final data
            var xPos2 = 230;
            var yPos1 = -65;
            var yPos2 = -99;

            var heights = new List<int>();
            var result = 0;

            for (int y = 1; y <= 500; y++)          // iterate through a bunch of y trajectories
            {
                for (int x = 1; x <= 500; x++)      // iterate through a bunch of x trajectories
                {
                    var yHeight = 0;
                    var incr = y;
                    var maxHeight = 0;

                    do
                    {
                        yHeight += incr;

                        if (incr == 0)
                            maxHeight = yHeight;

                        incr--;

                        if (yHeight <= yPos1 && yHeight >= yPos2)
                            heights.Add(maxHeight);

                    } while (yHeight >= yPos2);

                }
            }

            result = heights.Max(h => h);

            return result.ToString();
        }

        [TestCase("Test01", "112")]
        //[TestCase("Final", "1739")]
        public override string Part02(string[] rawInput)
        {
            //target area: x=20..30, y=-10..-5      // test
            //target area: x=201..230, y=-99..-65   // final

            var input = rawInput.ToList();          // skipped the parsing and just directly input the values

            //var xPos1 = 20;                         // test
            //var xPos2 = 30;
            //var yPos1 = -5;
            //var yPos2 = -10;

            var xPos1 = 201;                        // final
            var xPos2 = 230;
            var yPos1 = -65;
            var yPos2 = -99;

            var heights = new List<int>();
            var initialVelocities = new List<Point>();

            for (int y = -500; y <= 1000; y++)          // iterate through a bunch of y trajectories
            {
                for (int x = 1; x <= 1000; x++)          // iterate through a bunch of x trajectories
                {
                    var xPos = 0;
                    var yPos = 0;
                    var xIncr = x;
                    var yIncr = y;

                    do
                    {
                        xPos += xIncr;
                        yPos += yIncr;

                        if (xPos >= xPos1 && xPos <= xPos2 &&
                            yPos <= yPos1 && yPos >= yPos2)
                        {
                            initialVelocities.Add(new Point(x, y));
                        }

                        if (xIncr > 0)
                            xIncr--;
                        yIncr--;

                    } while (xPos <= xPos2 && yPos >= yPos2);

                }
            }

            initialVelocities = initialVelocities.Distinct().OrderBy(x => x.X).ThenBy(x => x.Y).ToList();

            return initialVelocities.ToString();
        }
    }
}