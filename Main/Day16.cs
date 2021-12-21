using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day16 : Day
    {
        public static readonly string[] Operations = new string[] { "+", "*", "↓", "↑", "", ">", "<", "=" };

        //[TestCase("Test01", "?")]
        //[TestCase("Final", "?")]
        public override string Part01(string[] rawInput)
        {
            //var input = rawInput.ToList();
            var input = new List<string>() { "38006F45291200" };
            var binary = String.Join("", input[0].Select(c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0')).ToList());
            var result = 0l;
            var pointer = 0;
            var number = new List<string>();

            var packetVersion = "";
            var lengthTypeId = "";
            var lengthType = "";
            var op = "";
            var length = "";


            while (pointer < binary.Length - 1)
            {
                packetVersion = binary.Substring(pointer, 3);                                           // Version
                pointer += 3;
                result += Convert.ToInt64(packetVersion, 2);

                lengthTypeId = binary.Substring(pointer, 3);                                            // Type
                pointer += 3;

                if (lengthTypeId == "100")                                                              // Literal
                {
                    var value = "";

                    while (binary[pointer] == '1')
                    {
                        value += binary.Substring(pointer + 1, 4);
                        pointer += 5;
                    }

                    value += binary.Substring(pointer + 1, 4);
                    pointer += 5;

                    //Console.WriteLine("Literal: " + Convert.ToInt64(value, 2));

                    if (binary.Substring(pointer).All(ch => ch == '0'))                                 // remove trailing zeros
                        binary = "";
                }
                else                                                                                    // operator
                {
                    lengthTypeId = binary.Substring(pointer, 1);
                    pointer += 1;

                    if (lengthTypeId == "0")                                                            // 0 = the next 15 bits are length
                    {
                        length = binary.Substring(pointer, 15);
                        //Console.WriteLine("Op15: " + Convert.ToInt64(binary.Substring(pointer, 15)), 2);
                        pointer += 15;

                        if (binary != "" && binary.Substring(pointer).All(ch => ch == binary[0]))       // remove trailing zeros
                            binary = "";
                    }
                    else                                                                                // 1 = the next 11 bits are the number of sub-packets
                    {
                        lengthType = binary.Substring(pointer, 11);
                        //Console.WriteLine("Op11: " + Convert.ToInt64(binary.Substring(pointer, 11), 2));
                        pointer += 11;

                        if (binary != "" && binary.Substring(pointer).All(ch => ch == binary[0]))       // remove trailing zeros
                            binary = "";
                    }
                }
            };

            return result.ToString();
        }

        //[TestCase("Test01", "")]
        [TestCase("Final", "1264857437203")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            //var input = new List<string>() { "9C005AC2F8F0" };
            var binary = String.Join("", input[0].Select(c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0')).ToList());
            var result = 0l;
            var pointer = 0;
            var numbers = new List<long>();

            result = Parse(ref binary, ref pointer, 99999, 99999, ref numbers);

            return result.ToString();
        }

        private long Parse(ref string binary, ref int pointer, int packetCountTarget, int packetCountLength, ref List<long> numbers)
        {
            while ((binary != "" && pointer < binary.Length - 1)  && packetCountTarget > 0 && pointer < packetCountLength)
            {
                if (binary.Substring(pointer + 3, 3) == "100")
                {
                    pointer += 6;
                    Literal(ref binary, ref pointer, ref numbers);
                    packetCountTarget--;
                }
                else if (binary.Substring(pointer + 6, 1) == "0")
                {
                    pointer += 7;
                    numbers.Add(OpLength(ref binary, ref pointer));
                    packetCountTarget--;
                }
                else
                {
                    pointer += 7;
                    numbers.Add(OpPacket(ref binary, ref pointer));
                    packetCountTarget--;
                }

                if (binary != "" && pointer < binary.Length)
                {
                    var val = binary.Substring(pointer, 1);
                    if (binary.Substring(pointer).All(ch => ch == Convert.ToChar(val)))
                    {
                        binary = "";
                        pointer = binary.Length;
                    }
                }
            };

            return numbers[0];
        }

        private void Literal(ref string binary, ref int pointer, ref List<long> numbers)
        {
            var value = "";

            while (binary[pointer] == '1')
            {
                value += binary.Substring(pointer + 1, 4);
                pointer += 5;
            }

            value += binary.Substring(pointer + 1, 4);
            pointer += 5;
            numbers.Add(Convert.ToInt64(value, 2));
        }

        private long OpLength(ref string binary, ref int pointer)
        {
            var operation = Operations[Convert.ToInt32(binary.Substring(pointer - 4, 3), 2)];
            var numbers = new List<long>();

            var length = Convert.ToInt32(binary.Substring(pointer, 15), 2);
            pointer += 15;
            Parse(ref binary, ref pointer, 99999,  pointer + length, ref numbers);

            if (binary != "" && pointer < binary.Length)
            {
                var val = binary.Substring(pointer, 1);
                if (binary.Substring(pointer).All(ch => ch == Convert.ToChar(val)))
                {
                    binary = "";
                    pointer = binary.Length;
                }
            }

            return doOpertation(operation, numbers);
        }

        private long OpPacket(ref string binary, ref int pointer)
        {
            var operation = Operations[Convert.ToInt32(binary.Substring(pointer - 4, 3), 2)];
            var numbers = new List<long>();

            var length = Convert.ToInt32(binary.Substring(pointer, 11), 2);
            pointer += 11;
            Parse(ref binary, ref pointer, length, 99999, ref numbers);

            return doOpertation(operation, numbers);
        }

        private long doOpertation(string operation, List<long> numbers)
        {
            long returnValue = 0;

            if (numbers.Count == 1)
                return numbers[0];

            switch (operation)
            {
                case "+":
                    returnValue = numbers.Sum(n => n);
                    break;
                case "*":
                    returnValue = numbers.Aggregate((x, y) => x * y);
                    break;
                case "↓":
                    returnValue = numbers.Min(n => n);
                    break;
                case "↑":
                    returnValue = numbers.Max(n => n);
                    break;
                case ">":
                    returnValue = Convert.ToInt64(numbers[0] > numbers[1]);
                    break;
                case "<":
                    returnValue = Convert.ToInt64(numbers[0] < numbers[1]);
                    break;
                case "=":
                    returnValue = Convert.ToInt64(numbers[0] == numbers[1]);
                    break;
            }

            return returnValue;
        }
    }
}