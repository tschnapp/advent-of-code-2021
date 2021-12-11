using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day11 : Day
    {
        [TestCase("Test01", "1656")]
        [TestCase("Final", "1608")]
        public override string Part01(string[] rawInput)
        {
            var grid = new List<List<int>>();
            var adjacentSquares = new List<Point>() { new Point(-1, -1), new Point(0, -1), new Point(1, -1), new Point(-1, 0), new Point(1, 0), new Point(-1, 1), new Point(0, 1), new Point(1, 1) };
            var flashCount = 0;
            var hasFlashed = false;

            // Populate grid
            foreach (string input in rawInput)
                grid.Add(Enumerable.Range(0, input.Length / 1).Select(i => input.Substring(i * 1, 1)).Select(Int32.Parse).ToList());

            // Run through 100 steps
            for (int s = 0; s < 100; s++)
            {
                // Increment energy level
                for (int y = 0; y < 10; y++)
                    for (int x = 0; x < 10; x++)
                        grid[y][x]++;

                // Check for flashes
                do
                {
                    hasFlashed = false;

                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (grid[y][x] > 9)                             // Flash occurred
                            {
                                hasFlashed = true;
                                flashCount++;
                                grid[y][x] = 0;

                                foreach (Point c in adjacentSquares)        // Increment adjacent squares 
                                    if ((y + c.Y >= 0 && y + c.Y < 10 && x + c.X >= 0 && x + c.X < 10))     // Where not out of range
                                        if (grid[y + c.Y][x + c.X] != 0)    // And where they haven't already flashed
                                            grid[y + c.Y][x + c.X]++;
                            }
                        }
                    }
                } while (hasFlashed);
            }

            return flashCount.ToString();
        }

        [TestCase("Test01", "195")]
        [TestCase("Final", "214")]
        public override string Part02(string[] rawInput)
        {
            var grid = new List<List<int>>();
            var adjacentSquares = new List<Point>() { new Point(-1, -1), new Point(0, -1), new Point(1, -1), new Point(-1, 0), new Point(1, 0), new Point(-1, 1), new Point(0, 1), new Point(1, 1) };
            var hasFlashed = false;

            // Populate grid
            foreach (string input in rawInput)
                grid.Add(Enumerable.Range(0, input.Length / 1).Select(i => input.Substring(i * 1, 1)).Select(Int32.Parse).ToList());

            // Run through 1000 steps
            for (int s = 0; s < 1000; s++)
            {
                // Increment energy level
                for (int y = 0; y < 10; y++)
                    for (int x = 0; x < 10; x++)
                        grid[y][x]++;

                // Check for flashes
                do
                {
                    hasFlashed = false;

                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (grid[y][x] > 9)                             // Flash occurred
                            {
                                hasFlashed = true;
                                grid[y][x] = 0;

                                foreach (Point c in adjacentSquares)        // Increment adjacent squares 
                                    if ((y + c.Y >= 0 && y + c.Y < 10 && x + c.X >= 0 && x + c.X < 10))     // Where not out of range
                                        if (grid[y + c.Y][x + c.X] != 0)    // And where they haven't already flashed
                                            grid[y + c.Y][x + c.X]++;
                            }
                        }
                    }
                } while (hasFlashed);

                // Check for flash synchronization
                var isSynchronized = true;

                for (int y = 0; y < 10; y++)
                    for (int x = 0; x < 10; x++)
                        if (grid[y][x] != 0)
                            isSynchronized = false;

                if (isSynchronized)
                    return (s + 1).ToString();
            }

            return "";
        }
    }
}