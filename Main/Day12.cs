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
        [TestCase("Test02", "103")]
        [TestCase("Test03", "3509")]
        [TestCase("Final", "84271")]
        public override string Part02(string[] rawInput)
        {
            var inputData = rawInput.ToList();
            var caves = new List<Cave2>();
            var caveResults = new List<string>();

            foreach (string input in inputData)
            {
                var splitInput = input.Split("-");

                var caveIndex = caves.FindIndex(c => c.Id == splitInput[0]);        // Build connections for the cave on the left side of the data

                if (caveIndex == -1)
                    caves.Add(new Cave2(splitInput[0]));                            // Add left cave if not exist

                caveIndex = caves.FindIndex(c => c.Id == splitInput[0]);            
                if (!caves[caveIndex].Connections.Contains(splitInput[1]) && splitInput[1] != "start")
                    caves[caveIndex].Connections.Add(splitInput[1]);                // Add right connection if not exist and not "start"

                caveIndex = caves.FindIndex(c => c.Id == splitInput[1]);            // Build connections for the cave on the right side of the data

                if (caveIndex == -1)
                    caves.Add(new Cave2(splitInput[1]));                            // Add right cave if not exist

                caveIndex = caves.FindIndex(c => c.Id == splitInput[1]);
                if (!caves[caveIndex].Connections.Contains(splitInput[0]) && splitInput[0] != "start")
                    caves[caveIndex].Connections.Add(splitInput[0]);                // Add left connection if not exist and not "start"
            }

            foreach (Cave2 cave in caves)
            {
                foreach (Cave2 c in caves)
                    c.TimesCanVisit = 1;

                if (!cave.IsBig && cave.Id != "start" && cave.Id != "end")          // Run process for each small cave, each time one small cave is given an extra visit
                {
                    cave.TimesCanVisit++;
                    exploreCaves2("start", new List<Cave2>(caves), new List<string>());
                }
            }

            caveResults = Globals.allPaths.Distinct().ToList();
            Globals.allPaths.Clear();

            return (caveResults.Count()).ToString();
        }

        private void exploreCaves2(string caveId, List<Cave2> caves, List<string> visitedCaves)
        {
            var currentCave = caves[caves.FindIndex(c => c.Id == caveId)];

            visitedCaves.Add(currentCave.Id);

            if (!currentCave.IsBig && currentCave.TimesCanVisit != 0)               // Decrementing TimesCanVisit
                currentCave.TimesCanVisit--;

            foreach (string cave in currentCave.Connections)
            {
                var targetCaveIndex = caves.FindIndex(c => c.Id == cave);

                if (caves[targetCaveIndex].TimesCanVisit > 0)                       // If this cave can stil be visited
                {
                    if (cave == "end")                                              // Found the end, add to list
                    {
                        Globals.allPaths.Add(string.Join("-", visitedCaves));
                        continue;
                    }
                    else                                                            // Found another path to take
                    {
                        var cavesCopy = new List<Cave2>(caves.Select(x => new Cave2(x.Id, x.Connections, x.IsBig, x.TimesCanVisit)));   // Create a copy to pass in

                        exploreCaves2(cave, cavesCopy, new List<string>(visitedCaves));
                    }
                }
            }
        }

        public class Cave2
        {
            public string Id { get; set; }
            public List<string> Connections { get; set; }
            public bool IsBig { get; set; }
            public int TimesCanVisit { get; set; }

            public Cave2(string id)
            {
                Id = id;
                Connections = Connections = new List<string>();
                IsBig = Id[0] < 91;
                TimesCanVisit = 1;
            }

            public Cave2(string id, List<string> connections, bool isBig, int timesCanVisit)
            {
                Id = id;
                Connections = connections;
                IsBig = isBig;
                TimesCanVisit = timesCanVisit;
            }
        }
    }
}