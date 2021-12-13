using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day12 : Day
    {
        //[TestCase("Test01", "10")]
        //[TestCase("Test02", "19")]
        //[TestCase("Test03", "226")]
        //[TestCase("Final", "3576")]
        public override string Part01(string[] rawInput)
        {
            var inputData = rawInput.ToList();
            var caves = new List<Cave>();
            var cavePaths = 0;

            foreach (string input in inputData)
            {
                var splitInput = input.Split("-");

                var caveIndex = caves.FindIndex(c => c.Id == splitInput[0]);        // Build connections for the cave on the left side of the data
                if (caveIndex == -1)
                    caves.Add(new Cave(splitInput[0], splitInput[1]));
                else
                    if (!caves[caveIndex].Connections.Contains(splitInput[0]))
                        caves[caveIndex].Connections.Add(splitInput[1]);

                caveIndex = caves.FindIndex(c => c.Id == splitInput[1]);            // Build connections for the cave on the right side of the data
                if (caveIndex == -1)
                    caves.Add(new Cave(splitInput[1], splitInput[0]));
                else
                    if (!caves[caveIndex].Connections.Contains(splitInput[1]))
                    caves[caveIndex].Connections.Add(splitInput[0]);
            }

            cavePaths = exploreCaves("start", caves, new List<string>());

            return cavePaths.ToString();
        }

        public class Cave
        {
            public string Id { get; set; }
            public List<string> Connections { get; set; }
            public bool IsBigCave { get; set; }

            public Cave(string id, string connectiom)
            {
                Id = id;
                Connections = Connections = new List<string>() { connectiom };
                IsBigCave = Id[0] < 91;
            }
        }

        private int exploreCaves(string caveId, List<Cave> cavesInput, List<string> visitedCavesInput)
        {
            var caves = new List<Cave>(cavesInput);
            var visitedCaves = new List<string>(visitedCavesInput);

            var caveIndex = caves.FindIndex(c => c.Id == caveId);
            var completedPath = 0;

            visitedCaves.Add(caveId);

            foreach (string cave in caves[caveIndex].Connections)
            {
                var adacentCaveIndex = caves.FindIndex(c => c.Id == cave);
                if (!visitedCaves.Contains(cave) ||                       // Has not been visited
                    caves[adacentCaveIndex].IsBigCave)
                {
                    if (cave == "end")
                    {
                        completedPath++;
                        //Console.WriteLine(string.Join(" - ", visitedCaves));
                    }
                    else
                    {
                        completedPath += exploreCaves(cave, caves, visitedCaves);
                    }
                }
            }

            return completedPath;
        }

        [TestCase("Test01", "36")]
        //[TestCase("Final", "103")]
        public override string Part02(string[] rawInput)
        {
            var inputData = rawInput.ToList();
            var caves = new List<Cave>();
            var cavePaths = 0;

            foreach (string input in inputData)
            {
                var splitInput = input.Split("-");

                var caveIndex = caves.FindIndex(c => c.Id == splitInput[0]);        // Build connections for the cave on the left side of the data
                if (caveIndex == -1)
                    caves.Add(new Cave(splitInput[0], splitInput[1]));
                else
                    if (!caves[caveIndex].Connections.Contains(splitInput[0]))
                    caves[caveIndex].Connections.Add(splitInput[1]);

                caveIndex = caves.FindIndex(c => c.Id == splitInput[1]);            // Build connections for the cave on the right side of the data
                if (caveIndex == -1)
                    caves.Add(new Cave(splitInput[1], splitInput[0]));
                else
                    if (!caves[caveIndex].Connections.Contains(splitInput[1]))
                    caves[caveIndex].Connections.Add(splitInput[0]);
            }

            cavePaths = exploreCaves2("start", caves, new List<string>());

            return cavePaths.ToString();
        }

        private int exploreCaves2(string caveId, List<Cave> cavesInput, List<string> visitedCavesInput)
        {
            var caves = new List<Cave>(cavesInput);
            var visitedCaves = new List<string>(visitedCavesInput);

            var caveIndex = caves.FindIndex(c => c.Id == caveId);
            var completedPath = 0;

            visitedCaves.Add(caveId);

            foreach (string cave in caves[caveIndex].Connections)
            {
                var adacentCaveIndex = caves.FindIndex(c => c.Id == cave);
                if (!visitedCaves.Contains(cave) ||                       // Has not been visited
                    caves[adacentCaveIndex].IsBigCave)
                {
                    if (cave == "end")
                    {
                        completedPath++;
                        Console.WriteLine(string.Join(" - ", visitedCaves));
                    }
                    else
                    {
                        completedPath += exploreCaves2(cave, caves, visitedCaves);
                    }
                }
            }

            return completedPath;
        }

    }
}