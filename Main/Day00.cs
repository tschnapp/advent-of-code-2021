using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day00 : Day
    {
        [TestCase("Test01", "?")]
        [TestCase("Final", "?")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.Select(Int32.Parse).ToList();

            return "";
        }

        [TestCase("Test01", "?")]
        [TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.Select(Int32.Parse).ToList();

            return "";
        }
    }
}