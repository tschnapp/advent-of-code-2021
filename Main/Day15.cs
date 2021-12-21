using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day15 : Day
    {
        [TestCase("Test01", "40")]
        [TestCase("Final", "?")]
        public override string Part01(string[] rawInput)
        {
            var rawInputSplit = rawInput.ToList();
            var input = new List<List<int>>();

            for (int i = 0; i < rawInputSplit.Count(); i++)
            {
                input.Add(new List<int>());
                for (int c = 0; c < rawInputSplit[i].Count(); c++)
                {
                    input[i].Add(Int32.Parse(rawInputSplit[i][c].ToString()));
                }
            }

            var caveXMax = input[0].Count();
            var caveYMax = input.Count();
            //var paddingX = caveXMax.ToString().Length;
            //var paddingY = caveYMax.ToString().Length;
            var dijkstraTable = new int[caveXMax, caveYMax];     // new List<DijkstraEntry>();

            dijkstraTable[0, 0] = input[0][0];

            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input.Count(); x++)
                {
                    if (x < caveXMax - 1)                       // set right
                    {
                        dijkstraTable[x + 1, y] = dijkstraTable[x, y] + input[y][x + 1];
                    }

                    if (y < caveYMax - 1)                       // set down
                    {
                        dijkstraTable[x, y + 1] = dijkstraTable[x, y] + input[y + 1][x];
                    }

                    if (x > 0 && y > 0)                         // choose if path left or up is less
                    {
                        dijkstraTable[x, y] = dijkstraTable[x - 1, y] < dijkstraTable[x, y - 1]
                            ? dijkstraTable[x - 1, y] + input[y][x]
                            : dijkstraTable[x, y - 1] + input[y][x];
                    }
                }
            }

            for (int y = 0; y < caveYMax; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < caveXMax; x++)
                {
                    Console.Write(dijkstraTable[x, y].ToString().PadLeft(3, ' ') + " ");
                }
            }

            return (dijkstraTable[caveXMax - 1, caveYMax - 1] - dijkstraTable[0, 0]).ToString();
        }

        public class DijkstraEntry
        {
            public string Vertex { get; set; }
            public int ShortestPath { get; set; }
            public string PreviousVertex { get; set; }

            public DijkstraEntry(string vertex, int shortestPath = 99999, string previousVertex = "")
            {
                Vertex = vertex;
                ShortestPath = shortestPath;
                PreviousVertex = previousVertex;
            }
        }

        //[TestCase("Test01", "40")]
        //[TestCase("Final", "?")]
        //public override string Part01(string[] rawInput)      // Original
        //{
        //    var input = rawInput.ToList();
        //    var caveXMax = input[0].Count();
        //    var caveYMax = input.Count();
        //    var cave = new int[caveXMax, caveYMax];

        //    for (int y = 0; y < input.Count(); y++)
        //        for (int x = 0; x < input.Count(); x++)
        //            cave[x, y] = (int)Char.GetNumericValue(input[y][x]);

        //    paths(new Point(0, 0), cave, 0);

        //    var shortestPath = Globals.shortestPath;

        //    return (shortestPath - 1).ToString();
        //}


        //private void paths(Point caveLoc, int[,] cave, int count)
        //{
        //    var caveXMax = cave.GetLength(0) - 1;
        //    var caveYMax = cave.GetLength(1) - 1;
        //    count += cave[caveLoc.X, caveLoc.Y];

        //    if (caveLoc.X == caveXMax && caveLoc.Y == caveYMax)
        //    {
        //        Globals.shortestPath = Globals.shortestPath < count ? Globals.shortestPath : count;
        //        return;
        //    }

        //    if (caveLoc.X < caveXMax)
        //        paths(new Point(caveLoc.X + 1, caveLoc.Y), cave, count);

        //    if (caveLoc.Y < caveYMax)
        //        paths(new Point(caveLoc.X, caveLoc.Y + 1), cave, count);
        //}

        //[TestCase("Test01", "40")]
        //[TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var caveXMax = input[0].Count();
            var caveYMax = input.Count();
            var cave = new int[caveXMax, caveYMax];

            for (int y = 0; y < input.Count(); y++)
                for (int x = 0; x < input.Count(); x++)
                    cave[x, y] = (int)Char.GetNumericValue(input[y][x]);

            //paths(new Point(0, 0), cave, 0);

            var shortestPath = Globals.shortestPath;

            return (shortestPath - 1).ToString();
        }
    }
}