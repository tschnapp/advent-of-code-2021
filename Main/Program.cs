using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;

            var mostRecentDay = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Day)))
                .OrderByDescending(t => int.Parse(t.Name.Replace("Day", "")))
                //.First();
                .First(t => t.Name == "Day16");   // specify day

            var day = Activator.CreateInstance(mostRecentDay) as Day;

            mostRecentDay.GetMethods()
                .Where(m => m.Name == "Part01")
                .SelectMany(m => m.GetCustomAttributes(typeof(TestCaseAttribute), false))
                .Cast<TestCaseAttribute>()
                .ToList()
                .ForEach(t => day.Part01(t.Version, t.ExpectedResult));

            mostRecentDay.GetMethods()
                .Where(m => m.Name == "Part02")
                .SelectMany(m => m.GetCustomAttributes(typeof(TestCaseAttribute), false))
                .Cast<TestCaseAttribute>()
                .ToList()
                .ForEach(t => day.Part02(t.Version, t.ExpectedResult));

            Console.WriteLine($"{System.Environment.NewLine}Program run time: {DateTime.Now.Subtract(startTime).ToString(@"hh\:mm\:ss")}");

            Console.ReadKey();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    class TestCaseAttribute : Attribute
    {
        public string Version { get; }
        public string ExpectedResult { get; }

        public TestCaseAttribute(string version, string expectedResult)
        {
            Version = version;
            ExpectedResult = expectedResult;
        }
    }

    abstract class Day
    {
        public void Part01(string version, string expectedResult)
        {
            var (filename, rawInput) = this.GetInput(version);
            var result = Part01(rawInput);
            Globals.ReportResults(filename, result, expectedResult);
        }

        public abstract string Part01(string[] rawInput);

        public void Part02(string version, string expectedResult)
        {
            var (filename, rawInput) = this.GetInput(version);
            var result = Part02(rawInput);
            Globals.ReportResults(filename, result, expectedResult);
        }

        public abstract string Part02(string[] rawInput);
    }

    public static class Globals
    {
        public const string PATH = "./../../../data/";

        public static void ReportResults(string filename, string result, string expectedResult)
        {
            Console.WriteLine($"File: {filename}, Result: {result}, Expected Result: {expectedResult} ...{(result == expectedResult ? "PASSED" : "FAILED")}");
        }

        public static (string, string[]) GetInput<T>(this T self, string version, [CallerMemberName] string methodName = "")
        {
            var filename = self.GetType().Name + "_" + methodName + "_" + version + ".txt";
            var filepath = Globals.PATH + filename;
            var input = System.IO.File.ReadAllLines(filepath);

            return (filename, input);
        }

        public static List<List<bool>> hasBeenVisited = new List<List<bool>>();

        public static List<string> allPaths = new List<string>();

        public static int shortestPath = 9999;
    }
}
