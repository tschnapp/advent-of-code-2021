using System;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;


            #region Previous Days
            var day01 = new Day01();
            day01.Part01("Test01", "7");
            day01.Part01("Final", "1752");

            day01.Part02("Test01", "5");
            day01.Part02("Final", "1781");
            #endregion

            //var day02 = new Day01();
            //day02.Part01("Test01", "7");
            //day02.Part01("Final", "?");

            //var day02 = new Day01();
            //day02.Part02("Test01", "5");
            //day02.Part02("Final", "?");


            Console.WriteLine(Environment.NewLine + "Program run time: " + string.Format(@"{0:hh\:mm\:ss}", (DateTime.Now - startTime)));
            Console.ReadKey();
        }
    }

    public static class Globals
    {
        public const string PATH = "./../../../data/";
    }
}
