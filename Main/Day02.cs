using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day02
    {
        public void Part01(string version, string expectedResult)
        {
            var filename = this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name + "_" + version + ".txt";
            var filepath = Globals.PATH + filename;
            var data = System.IO.File.ReadAllText(filepath).Split(Environment.NewLine);
            var result = "";


            //result = largerMeasurements.ToString();
            Console.WriteLine("File: " + filename + ", Result:" + result + ", Expected Result: " + expectedResult + " ..." + (result == expectedResult ? "PASSED" : "FAILED"));
        }

        public void Part02(string version, string expectedResult)
        {
            var filename = this.GetType().Name + "_" + MethodBase.GetCurrentMethod().Name + "_Ver" + version + ".txt";
            var filepath = Globals.PATH + filename;
            var data = System.IO.File.ReadAllText(filepath).Split(Environment.NewLine);
            var result = "";


            //result = largerMeasurements.ToString();
            Console.WriteLine("File: " + filename + ", Result: " + result + ", Expected Result: " + expectedResult + " ..." + (result == expectedResult ? "PASSED" : "FAILED"));
        }
    }
}
