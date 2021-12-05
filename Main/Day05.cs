using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day05 : Day
    {
        [TestCase("Test01", "5")]
        [TestCase("Final", "7468")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            List<Point> startPoints = new List<Point>();
            List<Point> endPoints = new List<Point>();
            var twoLinesOverlapCount = 0;

            // Get points
            foreach (string i in input)
            {
                var coordindates = i.Split(" -> ");
                var coordinate = coordindates[0].Split(',').Select(Int32.Parse).ToList();
                startPoints.Add(new Point(coordinate[0], coordinate[1]));

                coordinate = coordindates[1].Split(',').Select(Int32.Parse).ToList();
                endPoints.Add(new Point(coordinate[0], coordinate[1]));
            }


            var mapMaxX = startPoints.Max(x => x.X) > endPoints.Max(x => x.X) ? startPoints.Max(x => x.X) + 1 : endPoints.Max(x => x.X) + 1;
            var mapMaxY = startPoints.Max(y => y.Y) > endPoints.Max(y => y.Y) ? startPoints.Max(y => y.Y) + 1 : endPoints.Max(y => y.Y) + 1;
            int[,] map = new int[mapMaxX, mapMaxY];

            // Populate map
            for (int i = 0; i < startPoints.Count(); i++)
            {
                if (startPoints[i].X == endPoints[i].X || startPoints[i].Y == endPoints[i].Y)
                {
                    if (startPoints[i].Y == endPoints[i].Y)             // Horizontal line
                    {
                        var startingValue = startPoints[i].X < endPoints[i].X ? startPoints[i].X : endPoints[i].X;
                        var lineLength = Math.Abs(startPoints[i].X - endPoints[i].X) + startingValue;

                        for (int x = startingValue; x <= lineLength; x++)
                        {
                            map[x, startPoints[i].Y]++;
                        }
                    }
                    else if (startPoints[i].X == endPoints[i].X)             // Vertical line
                    {
                        var startingValue = startPoints[i].Y < endPoints[i].Y ? startPoints[i].Y : endPoints[i].Y;
                        var lineLength = Math.Abs(startPoints[i].Y - endPoints[i].Y) + startingValue;

                        for (int y = startingValue; y <= lineLength; y++)
                        {
                            map[startPoints[i].X, y]++;
                        }
                    }
                }
            }

            // Get vent >= 2 counts
            for (int y = 0; y < mapMaxY; y++)
            {
                //Console.WriteLine();
                for (int x = 0; x < mapMaxX; x++)
                {
                    //Console.Write(map[x, y]);
                    if (map[x, y] >= 2)
                        twoLinesOverlapCount++;
                }
            }
            
            return twoLinesOverlapCount.ToString();
        }

        [TestCase("Test01", "12")]
        [TestCase("Final", "22364")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            List<Point> startPoints = new List<Point>();
            List<Point> endPoints = new List<Point>();
            var twoLinesOverlapCount = 0;

            // Get points
            foreach (string i in input)
            {
                var coordindates = i.Split(" -> ");
                var coordinate = coordindates[0].Split(',').Select(Int32.Parse).ToList();
                startPoints.Add(new Point(coordinate[0], coordinate[1]));

                coordinate = coordindates[1].Split(',').Select(Int32.Parse).ToList();
                endPoints.Add(new Point(coordinate[0], coordinate[1]));
            }

            var mapMaxX = startPoints.Max(x => x.X) > endPoints.Max(x => x.X) ? startPoints.Max(x => x.X) + 1 : endPoints.Max(x => x.X) + 1;
            var mapMaxY = startPoints.Max(y => y.Y) > endPoints.Max(y => y.Y) ? startPoints.Max(y => y.Y) + 1 : endPoints.Max(y => y.Y) + 1;
            int[,] map = new int[mapMaxX, mapMaxY];

            // Populate map
            for (int i = 0; i < startPoints.Count(); i++)
            {
                if (startPoints[i].Y == endPoints[i].Y)             // Horizontal line
                {
                    var startingValueX = startPoints[i].X < endPoints[i].X ? startPoints[i].X : endPoints[i].X;
                    var lineLength = Math.Abs(startPoints[i].X - endPoints[i].X) + startingValueX;

                    for (int x = startingValueX; x <= lineLength; x++)
                    {
                        map[x, startPoints[i].Y]++;
                    }
                }
                else if (startPoints[i].X == endPoints[i].X)             // Vertical line
                {
                    var startingValueY = startPoints[i].Y < endPoints[i].Y ? startPoints[i].Y : endPoints[i].Y;
                    var lineLength = Math.Abs(startPoints[i].Y - endPoints[i].Y) + startingValueY;

                    for (int y = startingValueY; y <= lineLength; y++)
                    {
                        map[startPoints[i].X, y]++;
                    }
                }
                else                                                    // Diagnal line
                {
                    var startingValueX = startPoints[i].X < endPoints[i].X ? startPoints[i].X : endPoints[i].X;
                    var startingValueY = startPoints[i].X < endPoints[i].X ? startPoints[i].Y : endPoints[i].Y;     // flip if x flipped
                    var lineLength = Math.Abs(startPoints[i].X - endPoints[i].X) + startingValueX;
                    var y = startingValueY;
                    var yDirection = startPoints[i].Y < endPoints[i].Y ? 1 : -1;
                    yDirection *= startPoints[i].X < endPoints[i].X ? 1 : -1;                                       // if the line flipped, reverse the direction

                    for (int x = startingValueX; x <= lineLength; x++)
                    {
                        map[x, y]++;
                        y += yDirection;
                    }
                }
            }

            // Get vent >= 2 counts
            for (int y = 0; y < mapMaxY; y++)
            {
                //Console.WriteLine();
                for (int x = 0; x < mapMaxX; x++)
                {
                    //Console.Write(map[x, y]);
                    if (map[x, y] >= 2)
                        twoLinesOverlapCount++;
                }
            }

            return twoLinesOverlapCount.ToString();
        }
    }
}