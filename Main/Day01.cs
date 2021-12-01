using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day01
    {
        public void Part01(string version, string expectedResult)
        {
            var filename = this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name + "_" + version + ".txt";
            var filepath = Globals.PATH + filename;
            var data = System.IO.File.ReadAllText(filepath).Split(Environment.NewLine).Select(Int32.Parse).ToList();
            var result = "";


            var largerMeasurements = 0;
            for (int i = 1; i < data.Count; i++)
                if (data[i] > data[i - 1])
                    largerMeasurements++;


            result = largerMeasurements.ToString();
            Console.WriteLine("File: " + filename + ", Result:" + result + ", Expected Result: " + expectedResult + " ..." + (result == expectedResult ? "PASSED" : "FAILED"));
        }

        public void Part02(string version, string expectedResult)
        {
            var filename = this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name + "_" + version + ".txt";
            var filepath = Globals.PATH + filename;
            var data = System.IO.File.ReadAllText(filepath).Split(Environment.NewLine).Select(Int32.Parse).ToList();
            var result = "";


            var largerMeasurements = 0;
            for (int i = 3; i < data.Count; i++)
                if ((data[i] + data[i - 1] + data[i - 2]) > (data[i - 1] + data[i - 2] + data[i - 3]))
                    largerMeasurements++;

            result = largerMeasurements.ToString();
            Console.WriteLine("File: " + filename + ", Result: " + result + ", Expected Result: " + expectedResult + " ..." + (result == expectedResult ? "PASSED" : "FAILED"));
        }
    }
}
