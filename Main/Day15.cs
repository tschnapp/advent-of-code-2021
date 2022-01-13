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

// My attempt to write my own Dijkstra algorthm was unsuccessful (above) so I implemented Dijkstra.NET in another solution and it worked, code below.

//// PART 1

//var input = System.IO.File.ReadAllLines("C:\\Users\\Brian Schnapp\\source\\repos\\Dijkstra.NET\\src\\Samples\\Dijkstra.Net40\\Day15.txt");
//var gridSize = Convert.ToUInt16(input.Length);
//var data = new int[gridSize, gridSize];
//var graph = new Graph<int, string>();

//for (int y = 0; y < gridSize; y++)                  // set up data
//    for (int x = 0; x < gridSize; x++)
//        data[y, x] = input[y][x] - '0';

//for (int y = 0; y < gridSize; y++)                  // create nodes
//    for (int x = 0; x < gridSize; x++)
//        graph.AddNode((y * gridSize) + x + 1);

//for (uint y = 0; y < gridSize; y++)                 // map edges
//{
//    for (uint x = 0; x < gridSize; x++)
//    {
//        if (x < gridSize - 1)                       // right
//        {
//            graph.Connect((y * gridSize) + x + 1, (y * gridSize) + x + 2, data[y, x + 1], "");
//            graph.Connect((y * gridSize) + x + 2, (y * gridSize) + x + 1, data[y, x], "");
//        }

//        if (y < gridSize - 1)                       // down
//        {
//            graph.Connect((y * gridSize) + x + 1, ((y + 1) * gridSize) + x + 1, data[y + 1, x], "");
//            graph.Connect(((y + 1) * gridSize) + x + 1, (y * gridSize) + x + 1, data[y, x], "");
//        }
//    }
//}

//ShortestPathResult result = graph.Dijkstra(1, gridSize);

//Console.WriteLine(result.IsFounded == false ? "PATH NOT FOUND" : (result.Distance).ToString());
//Console.ReadKey();      /// Answer: 294



//// PART 2

//var input = System.IO.File.ReadAllLines("C:\\Users\\Brian Schnapp\\source\\repos\\Dijkstra.NET\\src\\Samples\\Dijkstra.Net40\\Day15.txt");
//var gridSize = Convert.ToUInt16(input.Length * 5);
//var data = new int[gridSize, gridSize];
//var graph = new Graph<int, string>();
//int[,] incrementor = new int[,] {
//                { 0, 1, 2, 3, 4 },
//                { 1, 2, 3, 4, 5 },
//                { 2, 3, 4, 5, 6 },
//                { 3, 4, 5, 6, 7 },
//                { 4, 5, 6, 7, 8 }
//            };

//for (int my = 0; my < 5; my++)                      // set up data
//    for (int mx = 0; mx < 5; mx++)
//        for (int y = 0; y < gridSize; y++)
//            for (int x = 0; x < gridSize; x++)
//            {
//                var value = (input[y % 100][x % 100] - '0') + incrementor[x / 100, y / 100];
//                value = value > 9 ? value - 9 : value;
//                data[y, x] = value;
//            }

//for (int y = 0; y < gridSize; y++)                  // create nodes
//    for (int x = 0; x < gridSize; x++)
//        graph.AddNode((y * gridSize) + x + 1);

//for (uint y = 0; y < gridSize; y++)                 // map edges
//{
//    for (uint x = 0; x < gridSize; x++)
//    {
//        if (x < gridSize - 1)                       // right/left
//        {
//            graph.Connect((y * gridSize) + x + 1, (y * gridSize) + x + 2, data[y, x + 1], "");
//            graph.Connect((y * gridSize) + x + 2, (y * gridSize) + x + 1, data[y, x], "");
//        }

//        if (y < gridSize - 1)                       // down/up
//        {
//            graph.Connect((y * gridSize) + x + 1, ((y + 1) * gridSize) + x + 1, data[y + 1, x], "");
//            graph.Connect(((y + 1) * gridSize) + x + 1, (y * gridSize) + x + 1, data[y, x], "");
//        }
//    }
//}

//ShortestPathResult result = graph.Dijkstra(1, 250000);

//Console.WriteLine(result.IsFounded == false ? "PATH ON FOUND" : (result.Distance).ToString());
//Console.ReadKey();      /// Answer: 2806