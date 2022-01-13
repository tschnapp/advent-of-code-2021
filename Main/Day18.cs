using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{ 
    class Day18 : Day
    {
        //[TestCase("Test01", "4140")]
        //[TestCase("Final", "3987")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var result = input[0];

            for (int i = 1; i < input.Count; i++)
            {
                var addedValue = "[" + result + "," + input[i] + "]";
                result = Simplify(addedValue.Select(c => c.ToString()).ToList());
            }

            Console.WriteLine(result);

            return GetMagnitude(result.Select(c => c.ToString()).ToList());
        }

        [TestCase("Test01", "3993")]
        [TestCase("Final", "4500")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var result = input[0];
            var magnitudeResult = "";
            var largestMagnitude = 0;

            for (int j = 0; j < input.Count; j++)
            {
                for (int i = 0; i < input.Count - 1; i++)
                {
                    if (i != j)
                    {
                        var addedValue = "[" + input[i] + "," + input[j] + "]";
                        result = Simplify(addedValue.Select(c => c.ToString()).ToList());
                        magnitudeResult = GetMagnitude(result.Select(c => c.ToString()).ToList());
                        if (Convert.ToInt32(magnitudeResult) > largestMagnitude)
                            largestMagnitude = Convert.ToInt32(magnitudeResult);

                        addedValue = "[" + input[j] + "," + input[i] + "]";
                        result = Simplify(addedValue.Select(c => c.ToString()).ToList());
                        magnitudeResult = GetMagnitude(result.Select(c => c.ToString()).ToList());
                        if (Convert.ToInt32(magnitudeResult) > largestMagnitude)
                            largestMagnitude = Convert.ToInt32(magnitudeResult);
                    }
                }
            }

            Console.WriteLine(largestMagnitude);

            return largestMagnitude.ToString();
        }

        public string Simplify(List<string> input)
        {
            var leftBrace = 0;
            var changeMade = false;

            do
            {
                // explode
                do
                {
                    leftBrace = 0;
                    changeMade = false;

                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i] == "[")
                        {
                            leftBrace++;
                            continue;
                        }
                        else if (input[i] == "]")
                        {
                            leftBrace--;
                            continue;
                        }
                        else if (IsNumeric(input[i]) && input[i + 1] == "," && IsNumeric(input[i + 2]) && leftBrace > 4)
                        {
                            var pairToExplode = input.GetRange(i, 3);       // Get pair to explode
                            input.RemoveRange(i, 4);                        // Remove items where pair was
                            i--;
                            input[i] = "0";                                 // Keep insertion point

                            var indexForLeftNum = GetNeighborNum(input, i, "Left");
                            var indexForRightNum = GetNeighborNum(input, i, "Right");

                            if (indexForLeftNum != -1)                      // Add left number
                                input[indexForLeftNum] = (Convert.ToInt32(input[indexForLeftNum]) + Convert.ToInt32(pairToExplode[0])).ToString();

                            if (indexForRightNum != -1)                     // Add right number
                                input[indexForRightNum] = (Convert.ToInt32(input[indexForRightNum]) + Convert.ToInt32(pairToExplode[2])).ToString();

                            changeMade = true;
                            break;
                        }
                    }

                    //Console.WriteLine(String.Join("", input));

                } while (changeMade);



                // split
                changeMade = false;

                for (int i = 0; i < input.Count; i++)
                {
                    if (IsNumeric(input[i]) && Convert.ToInt32(input[i]) > 9)
                    {
                        var numberToSplit = input[i];

                        input[i] = "[";
                        input.InsertRange(i + 1, new List<string>() {
                            (Convert.ToInt32(numberToSplit) / 2).ToString(),
                            ",",
                            ((Convert.ToInt32(numberToSplit) + 1) / 2).ToString(),
                            "]"
                        });

                        changeMade = true;
                        break;
                    }
                }

                //Console.WriteLine(String.Join("", input));

            } while (changeMade);

            return String.Join("", input);
        }

        public bool IsNumeric(string input)
        {
            return input.All(char.IsNumber);
        }
        public int GetNeighborNum(List<string> input, int i, string direction)
        {
            var indexForLeftNum = - 1;

            if (direction == "Left")
            {
                for (int x = i - 1; x >= 0; x--)
                {
                    if (IsNumeric(input[x]))
                    {
                        indexForLeftNum = x;
                        break;
                    }
                }
            }
            else if (direction == "Right")
            {
                for (int x = i + 1; x < input.Count; x++)
                {
                    if (IsNumeric(input[x]))
                    {
                        indexForLeftNum = x;
                        break;
                    }
                }
            }

            return indexForLeftNum;
        }
         public string GetMagnitude(List<string> input)
        {
            var result = 0;
            var leftBraceCount = 0;
            var deepestLeftBrace = 0;
            var deepestLeftBraceIndex = 0;

            do
            {
                leftBraceCount = 0;
                deepestLeftBrace = 0;
                deepestLeftBraceIndex = 0;

                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i] == "[")
                    {
                        leftBraceCount++;
                        if (leftBraceCount > deepestLeftBrace)
                        {
                            deepestLeftBrace = leftBraceCount;
                            deepestLeftBraceIndex = i;
                        }
                    }
                    else if (input[i] == "]")
                    {
                        leftBraceCount--;
                    }
                }

                input[deepestLeftBraceIndex] = ((Convert.ToInt32(input[deepestLeftBraceIndex + 1]) * 3) +
                                               (Convert.ToInt32(input[deepestLeftBraceIndex + 3]) * 2)).ToString();

                input.RemoveRange(deepestLeftBraceIndex + 1, 4);

                //Console.WriteLine(String.Join("", input));

            } while (input.Contains("["));

            return String.Join("", input);
        }
   }
}