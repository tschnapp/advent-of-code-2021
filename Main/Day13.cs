using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day13 : Day
    {
        //[TestCase("Test01", "17")]
        //[TestCase("Final", "775")]
        public override string Part01(string[] rawInput)
        {
            var inputData = rawInput.ToList();
            var pageX = 0;
            var pageY = 0;
            var foldDataIndex = 0;
            var foldData = new List<string>();
            var dotCount = 0;

            for (int i = 0; i < inputData.Count(); i++)                                     // get largest x, y values
            {
                if (inputData[i] == "")
                    break;

                var coordinates = inputData[i].Split(",").Select(Int32.Parse).ToList();

                pageX = coordinates[0] > pageX ? coordinates[0] : pageX;
                pageY = coordinates[1] > pageY ? coordinates[1] : pageY;
            }

            pageX++;
            pageY++;

            var page = new int[pageX, pageY];                                               // create page based on largest x, y values

            for (int i = 0; i < inputData.Count(); i++)                                     // add dots to page
            {
                if (inputData[i] == "")
                {
                    foldDataIndex = i + 1;
                    break;
                }

                var coordinates = inputData[i].Split(",").Select(Int32.Parse).ToList();
                page[coordinates[0], coordinates[1]] = 1;
            }

            for (int i = foldDataIndex; i < inputData.Count(); i++)                         // get fold data
            {
                foldData.Add(inputData[i].Replace("fold along ", ""));
            }

            foreach (string fold in foldData)                                               // calculate folds
            {
                if (fold[0] == 'x')
                {
                    var foldPos = Int32.Parse(fold.Split("=")[1]);
                    var end = foldPos;
                    var start = end + 1;
                    end += start;

                    for (int x = start; x < end; x++)
                    {
                        for (int y = 0; y < pageY; y++)
                        {
                            if (page[x, y] == 1)
                            {
                                page[(foldPos * 2) - x, y] = 1;
                            }
                        }
                    }

                    pageX = foldPos;

                    dotCount = 0;
                    for (int y = 0; y < pageY; y++)
                    {
                        for (int x = 0; x < pageX; x++)
                        {
                            Console.Write(page[x, y]);
                            dotCount += page[x, y];
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else
                {
                    var foldPos = Int32.Parse(fold.Split("=")[1]);
                    var end = foldPos;
                    var start = end + 1;
                    end += start;

                    for (int y = start; y < end; y++)
                    {
                        for (int x = 0; x < pageX; x++)
                        {
                            if (page[x, y] == 1)
                            {
                                page[x, (foldPos * 2) - y] = 1;
                            }
                        }
                    }

                    pageY = foldPos;

                    dotCount = 0;
                    for (int y = 0; y < pageY; y++)
                    {
                        for (int x = 0; x < pageX; x++)
                        {
                            Console.Write(page[x, y]);
                            dotCount += page[x, y];
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                    return dotCount.ToString();
                }
            }

            for (int y = 0; y < pageY; y++)
                for (int x = 0; x < pageX; x++)
                    dotCount += page[x, y];

            return dotCount.ToString();
        }

        [TestCase("Final", "REUPUPKR")]
        public override string Part02(string[] rawInput)
        {
            var inputData = rawInput.ToList();
            var pageX = 0;
            var pageY = 0;
            var foldDataIndex = 0;
            var foldData = new List<string>();

            for (int i = 0; i < inputData.Count(); i++)                                     // get largest x, y values
            {
                if (inputData[i] == "")
                    break;

                var coordinates = inputData[i].Split(",").Select(Int32.Parse).ToList();

                pageX = coordinates[0] > pageX ? coordinates[0] : pageX;
                pageY = coordinates[1] > pageY ? coordinates[1] : pageY;
            }

            pageX++;
            pageY++;

            var page = new int[pageX, pageY];                                               // create page based on largest x, y values

            for (int i = 0; i < inputData.Count(); i++)                                     // add dots to page
            {
                if (inputData[i] == "")
                {
                    foldDataIndex = i + 1;
                    break;
                }

                var coordinates = inputData[i].Split(",").Select(Int32.Parse).ToList();
                page[coordinates[0], coordinates[1]] = 1;
            }

            for (int i = foldDataIndex; i < inputData.Count(); i++)                         // get fold data
            {
                foldData.Add(inputData[i].Replace("fold along ", ""));
            }

            foreach (string fold in foldData)                                               // calculate folds
            {
                if (fold[0] == 'x')
                {
                    var foldPos = Int32.Parse(fold.Split("=")[1]);
                    var end = foldPos;
                    var start = end + 1;
                    end += start;

                    for (int x = start; x < end; x++)
                    {
                        for (int y = 0; y < pageY; y++)
                        {
                            if (page[x, y] == 1)
                            {
                                page[(foldPos * 2) - x, y] = 1;
                            }
                        }
                    }

                    pageX = foldPos;
                }
                else
                {
                    var foldPos = Int32.Parse(fold.Split("=")[1]);
                    var start = foldPos + 1;
                    var end = foldPos + start - 1;

                    for (int y = start; y < end; y++)
                    {
                        for (int x = 0; x < pageX; x++)
                        {
                            if (page[x, y] == 1)
                            {
                                page[x, (foldPos * 2) - y] = 1;
                            }
                        }
                    }

                    pageY = foldPos;
                }
            }
            
            for (int y = 0; y < pageY; y++)                                         // display page
            {
                for (int x = 0; x < pageX; x++)
                {
                    Console.Write(page[x, y]);
                }
                Console.WriteLine();
            }

            return "REUPUPKR";                                                      // You had to interpret the text displayed on the page
        }
    }
}