using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day22 : Day
    {
        //[TestCase("Test01", "39")]
        //[TestCase("Test02", "590784")]
        //[TestCase("Final", "611176")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var space = new Space();

            foreach (string line in input)
            {
                var details = line.Split(new string[] { " x=", "..", ",y=", ",z=" }, StringSplitOptions.None);
                space.AddCoords(details[0], details[1], details[2], details[3], details[4], details[5], details[6]);
            }

            var results = space.CountSpaces();
            space.ClearSpaces();
            return results.ToString();
        }

        [TestCase("Test01", "2758514936282235")]
        //[TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var space = new Space();

            foreach (string line in input)
            {
                var details = line.Split(new string[] { " x=", "..", ",y=", ",z=" }, StringSplitOptions.None);
                space.AddCoords(details[0], details[1], details[2], details[3], details[4], details[5], details[6]);
            }

            var results = space.CountSpaces();
            space.ClearSpaces();
            return results.ToString();
        }
    }

    public class Space
    {
        public static bool[,,] SpaceCubes = new bool[101, 101, 101];

        public void AddCoords(string state, string x1, string x2, string y1, string y2, string z1, string z2)
        {
            var State = state == "on";
            var X1 = Convert.ToInt32(x1) + 50;
            var X2 = Convert.ToInt32(x2) + 50;
            var Y1 = Convert.ToInt32(y1) + 50;
            var Y2 = Convert.ToInt32(y2) + 50;
            var Z1 = Convert.ToInt32(z1) + 50;
            var Z2 = Convert.ToInt32(z2) + 50;

            X1 = X1 < 0 ? 0 : X1;
            X2 = X2 > 101 ? 101 : X2;
            Y1 = Y1 < 0 ? 0 : Y1;
            Y2 = Y2 > 101 ? 101 : Y2;
            Z1 = Z1 < 0 ? 0 : Z1;
            Z2 = Z2 > 101 ? 101 : Z2;

            for (int x = X1; x <= X2; x++)
            {
                for (int y = Y1; y <= Y2; y++)
                {
                    for (int z = Z1; z <= Z2; z++)
                    {
                        if (x >= 0 && x <= 100 && y >= 0 && y <= 100 && z >= 0 && z <= 100)
                            SpaceCubes[x, y, z] = State;
                    }
                }
            }
        }

        public int CountSpaces()
        {
            var count = 0;

            for (int x = 0; x <= 100; x++)
            {
                for (int y = 0; y <= 100; y++)
                {
                    for (int z = 0; z <= 100; z++)
                    {
                        if (SpaceCubes[x, y, z] == true)
                            count++;
                    }
                }
            }

            return count;
        }

        public void ClearSpaces()
        {
            for (int x = 0; x <= 100; x++)
            {
                for (int y = 0; y <= 100; y++)
                {
                    for (int z = 0; z <= 100; z++)
                    {
                        SpaceCubes[x, y, z] = false;
                    }
                }
            }
        }
    }
}