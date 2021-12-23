using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day20 : Day
    {
        [TestCase("Test01", "35")]
        [TestCase("Final", "5218")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var buffer = 10;
            var result = 0;
            var enhancementAlgorithm = input[0];
            var neighbors = new List<Point> {
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
                new Point(-1, 0), new Point(0, 0), new Point(1, 0),
                new Point(-1, 1), new Point(0, 1), new Point(1, 1)
            };

            var inputImage = input;
            inputImage.RemoveRange(0, 2);                               // remove header


            for (int l = 0; l < inputImage.Count(); l++)                // add left/right padding
                inputImage[l] = new String('.', buffer) + inputImage[l] + new String('.', buffer);

            var currentWidth = inputImage[0].Length;
            for (int i = 0; i < buffer; i++)
            {
                inputImage.InsertRange(0, new List<string>() { new String('.', currentWidth) });    // add top padding
                inputImage.AddRange(new List<string>() { new String('.', currentWidth) });    // add bottom padding
            }


            var outputImage = new List<string>(inputImage);             // copy input

            for (int s = 0; s < outputImage.Count(); s++)               // clear out output (copy of input)
                outputImage[s] = outputImage[s].Replace("#", ".");


            for (int i = 0; i < 2; i++)                                 // iterate twice
            {
                for (int y = 1; y < inputImage.Count() - 1; y++)
                {
                    for (int x = 1; x < inputImage[0].Length - 1; x++)
                    {
                        var binary = "";

                        foreach (Point n in neighbors)
                        {
                            binary += inputImage[y + n.Y][x + n.X] == '.' ? "0" : "1";
                        }

                        outputImage[y] = outputImage[y].Remove(x, 1).Insert(x, enhancementAlgorithm[Convert.ToInt32(binary, 2)].ToString());
                    }
                }
                inputImage = new List<string>(outputImage);
            }

            for (int y = buffer/2; y < outputImage.Count() - buffer / 2 - 1; y++)            // count them
            {
                var bufferedString = outputImage[y].Substring(buffer / 2, outputImage[y].Length - buffer);
                result += Convert.ToInt32(bufferedString.Count(c => c == '#'));
            }

            return result.ToString();
        }

        [TestCase("Test01", "3351")]
        [TestCase("Final", "15527")]
        public override string Part02(string[] rawInput)
        {
            // TODO: I thought of a better way to do this. Instead of giving extra padding for edge creep, I could just clear it out. Moving on for now.
            var input = rawInput.ToList();
            var buffer = 150;
            var iterations = 50;
            var result = 0;
            var enhancementAlgorithm = input[0];
            var neighbors = new List<Point> {
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
                new Point(-1, 0), new Point(0, 0), new Point(1, 0),
                new Point(-1, 1), new Point(0, 1), new Point(1, 1)
            };

            var inputImage = input;
            inputImage.RemoveRange(0, 2);                               // remove header


            for (int l = 0; l < inputImage.Count(); l++)                // add left/right padding
                inputImage[l] = new String('.', buffer) + inputImage[l] + new String('.', buffer);

            var currentWidth = inputImage[0].Length;
            for (int i = 0; i < buffer; i++)
            {
                inputImage.InsertRange(0, new List<string>() { new String('.', currentWidth) });    // add top padding
                inputImage.AddRange(new List<string>() { new String('.', currentWidth) });    // add bottom padding
            }


            var outputImage = new List<string>(inputImage);             // copy input

            for (int s = 0; s < outputImage.Count(); s++)               // clear out output (copy of input)
                outputImage[s] = outputImage[s].Replace("#", ".");


            for (int i = 0; i < iterations; i++)                        // iterations
            {
                for (int y = 1; y < inputImage.Count() - 1; y++)
                {
                    for (int x = 1; x < inputImage[0].Length - 1; x++)
                    {
                        var binary = "";

                        foreach (Point n in neighbors)                  // concatenate binary values
                        {
                            binary += inputImage[y + n.Y][x + n.X] == '.' ? "0" : "1";
                        }

                        outputImage[y] = outputImage[y].Remove(x, 1).Insert(x, enhancementAlgorithm[Convert.ToInt32(binary, 2)].ToString());
                    }
                }
                inputImage = new List<string>(outputImage);
            }

            for (int y = (buffer / 2); y < outputImage.Count() - (buffer / 2) - 1; y++)            // count them
            {
                var stringAfterBufferRemoved = outputImage[y].Substring((buffer / 2), outputImage[y].Length - buffer);
                result += Convert.ToInt32(stringAfterBufferRemoved.Count(c => c == '#'));
            }

            return result.ToString();
        }
    }
}