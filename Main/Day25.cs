using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day25 : Day
    {
        [TestCase("Test02", "58")]
        [TestCase("Final", "?")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var listToMove = new List<Point>();
            var cucumberMoved = false;
            var stepCount = 0;

            do
            {
                stepCount++;
                cucumberMoved = false;

                for (int y = 0; y < input.Count; y++)                                   // East movement - tag cucumber
                {
                    for (int x = 0; x < input[0].Length; x++)
                    {
                        if (x < input[0].Length - 1)
                        {
                            if (input[y][x] == '>' && input[y][x + 1] == '.')
                                listToMove.Add(new Point(x, y));
                        }
                        else
                        {
                            if (input[y][x] == '>' && input[y][0] == '.')
                                listToMove.Add(new Point(x, y));
                        }
                    }
                }

                foreach (Point item in listToMove)                                      // East movement - move cucumber
                {
                    if (item.X < input[0].Length - 1)
                        input[item.Y] = input[item.Y].Substring(0, item.X) + ".>" + input[item.Y].Substring(item.X + 2);
                    else
                        input[item.Y] = ">" + input[item.Y].Substring(1, item.X - 1) + ".";
                }

                if (listToMove.Count > 0)
                {
                    cucumberMoved = true;
                    listToMove.Clear();
                }

                for (int x = 0; x < input[0].Length; x++)                               // South movement - tag cucumber
                {
                    for (int y = 0; y < input.Count; y++)
                    {
                        if (y < input.Count - 1)
                        {
                            if (input[y][x] == 'v' && input[y + 1][x] == '.')
                                listToMove.Add(new Point(x, y));
                        }
                        else
                        {
                            if (input[y][x] == 'v' && input[0][x] == '.')
                                listToMove.Add(new Point(x, y));
                        }
                    }
                }

                foreach (Point item in listToMove)                                      // South movement - move cucumber
                {
                    if (item.Y < input.Count - 1)
                    {
                        input[item.Y] = input[item.Y].Substring(0, item.X) + "." + input[item.Y].Substring(item.X + 1);     // Prob should have use a char[,]
                        input[item.Y + 1] = input[item.Y + 1].Substring(0, item.X) + "v" + input[item.Y + 1].Substring(item.X + 1);
                    }
                    else
                    {
                        input[item.Y] = input[item.Y].Substring(0, item.X) + "." + input[item.Y].Substring(item.X + 1);
                        input[0] = input[0].Substring(0, item.X) + "v" + input[0].Substring(item.X + 1);
                    }
                }

                if (listToMove.Count > 0)
                {
                    cucumberMoved = true;
                    listToMove.Clear();
                }

                //for (int y = 0; y < input.Count; y++)
                //{
                //    Console.WriteLine(input[y]);
                //}
                //Console.WriteLine();

            } while (cucumberMoved);

            return stepCount.ToString();
        }

        //[TestCase("Test01", "?")]
        //[TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.Select(Int32.Parse).ToList();

            return "";
        }
    }
}