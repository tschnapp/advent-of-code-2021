using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day09 : Day
    {
        [TestCase("Test01", "15")]
        [TestCase("Final", "458")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var yMax = input.Count();
            var xMax = input[0].Count();
            var total = 0;

            for (int y = 0; y < yMax; y++)
                for (int x = 0; x < xMax; x++)
                    if ((x == 0        || input[y][x - 1] > input[y][x]) &&
                        (x == xMax - 1 || input[y][x + 1] > input[y][x]) &&
                        (y == 0        || input[y - 1][x] > input[y][x]) &&
                        (y == yMax - 1 || input[y + 1][x] > input[y][x]))
                    {
                        total += input[y][x] - 48 + 1;
                    }

            return total.ToString();
        }

        [TestCase("Test01", "1134")]
        [TestCase("Final", "1391940")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var yMax = input.Count();
            var xMax = input[0].Count();
            var Counts = new List<int>();
            var total = 0;
            List<CaveLocation> basins = new List<CaveLocation>();

            // Populate hasBeenVisited map.
            for (int y = 0; y < yMax; y++)
            {
                Globals.hasBeenVisited.Add(new List<bool>());
                for (int x = 0; x < xMax; x++)
                    Globals.hasBeenVisited[y].Add(false);
            }

            // Find basins (from part one).
            for (int y = 0; y < yMax; y++)
                for (int x = 0; x < xMax; x++)
                    if ((x == 0 || input[y][x - 1] > input[y][x]) &&
                        (x == xMax - 1 || input[y][x + 1] > input[y][x]) &&
                        (y == 0 || input[y - 1][x] > input[y][x]) &&
                        (y == yMax - 1 || input[y + 1][x] > input[y][x]))
                    {
                        basins.Add(new CaveLocation(x, y, false, false, false, false));
                    }

            // Starting with each basin, recursively check neighboring squares.
            for (int b = 0; b < basins.Count(); b++)
                Counts.Add(CheckLocations(basins[b], input, xMax, yMax));

            // Get top three basin counts.
            total = (from i in Counts
                     orderby i descending
                     select i).Take(3)
                     .Aggregate(1, (a, b) => a * b);

            return total.ToString();
        }

        private int CheckLocations(CaveLocation cl, List<string> input, int xMax, int yMax)
        {
            var total = 1;
            Globals.hasBeenVisited[cl.coordinate.Y][cl.coordinate.X] = true;

            // Instantiate new Cave Location in the neighboring 4 squares where valid low points exist.
            // Exclude borders, 9s, visited squares, and paths that take you back in the direction you started from

            if (!cl.leftChecked && cl.coordinate.X > 0)
                if (input[cl.coordinate.Y][cl.coordinate.X - 1].ToString() != "9" && !Globals.hasBeenVisited[cl.coordinate.Y][cl.coordinate.X - 1])
                    total += CheckLocations(new CaveLocation(cl.coordinate.X - 1, cl.coordinate.Y, false, true, false, false), input, xMax, yMax);
            cl.leftChecked = true;

            if (!cl.rightChecked && cl.coordinate.X < xMax - 1)
                if (input[cl.coordinate.Y][cl.coordinate.X + 1].ToString() != "9" && !Globals.hasBeenVisited[cl.coordinate.Y][cl.coordinate.X + 1])
                    total += CheckLocations(new CaveLocation(cl.coordinate.X + 1, cl.coordinate.Y, false, false, false, true), input, xMax, yMax);
            cl.rightChecked = true;

            if (!cl.upChecked && cl.coordinate.Y > 0)
                if (input[cl.coordinate.Y - 1][cl.coordinate.X].ToString() != "9" && !Globals.hasBeenVisited[cl.coordinate.Y - 1][cl.coordinate.X])
                    total += CheckLocations(new CaveLocation(cl.coordinate.X, cl.coordinate.Y - 1, false, false, true, false), input, xMax, yMax);
            cl.upChecked = true;

            if (!cl.downChecked && cl.coordinate.Y < yMax - 1)
                if(input[cl.coordinate.Y + 1][cl.coordinate.X].ToString() != "9" && !Globals.hasBeenVisited[cl.coordinate.Y + 1][cl.coordinate.X])
                    total += CheckLocations(new CaveLocation(cl.coordinate.X, cl.coordinate.Y + 1, true, false, false, false), input, xMax, yMax);
            cl.downChecked = true;

            return total;
        }

        public class CaveLocation
        {
            public Point coordinate { get; set; }
            public bool upChecked { get; set; }
            public bool rightChecked { get; set; }
            public bool downChecked { get; set; }
            public bool leftChecked { get; set; }

            public CaveLocation(int x, int y, bool up, bool right, bool down, bool left)
            {
                coordinate = new Point(x, y);
                upChecked = up;
                rightChecked = right;
                downChecked = down;
                leftChecked = left;
            }
        }
    }
}