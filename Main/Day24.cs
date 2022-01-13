using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day24 : Day
    {
        public long w { get; set; }
        public long x { get; set; }
        public long y { get; set; }
        public long z { get; set; }

        //[TestCase("Test01", "?")]
        [TestCase("Final", "?")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();

            return "";
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