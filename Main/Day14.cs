using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Main
{
    class Day14 : Day
    {
        [TestCase("Test01", "1588")]
        [TestCase("Final", "2549")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var template = input[0];
            var rules = input.GetRange(2, input.Count() - 2).Select(x => x.Split(" -> ")).ToList();
            var results = new List<Tuple<char, int>>();

            for (int l = 0; l < 10; l++)
            {
                var matchingRules = new List<Tuple<string, int>>();

                foreach (string[] rule in rules)
                {
                    for (int index = 0; ; index++)                                          // find rule matches in template
                    {
                        index = template.IndexOf(rule[0], index);
                        if (index == -1)
                            break;
                        matchingRules.Add(new Tuple<string, int>(rule[1], index + 1));      // add value to insert and location
                    }
                }

                matchingRules = matchingRules.OrderByDescending(t => t.Item2).ToList();

               foreach (Tuple<string, int> rule in matchingRules)                           // insert characters based on matching rules
                {
                    template = template.Insert(rule.Item2, rule.Item1);
                }
            }

            foreach (char letter in template.Distinct())                                // least and most populus characters
            {
                int freq = Regex.Matches(template, letter.ToString()).Count;
                results.Add(new Tuple<char, int>(letter, freq));
            }

            results = results.OrderBy(t => t.Item2).ToList();

            return (results[results.Count() - 1].Item2 - results[0].Item2).ToString();
        }

        [TestCase("Test01", "2188189693529")]
        [TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var template = input[0];
            var rules = input.GetRange(2, input.Count() - 2).Select(x => x.Split(" -> ")).ToList();
            var results = new List<Tuple<char, long>>();

            for (int l = 0; l < 40; l++)
            {
                Console.WriteLine(l + " " + template.Count() + " " + DateTime.Now);

                var matchingRules = new List<Tuple<string, int>>();

                foreach (string[] rule in rules)
                {
                    for (int index = 0; ; index++)                                          // find rule matches in template
                    {
                        index = template.IndexOf(rule[0], index);
                        if (index == -1)
                            break;
                        matchingRules.Add(new Tuple<string, int>(rule[1], index + 1));      // add value to insert and location
                    }
                }

                matchingRules = matchingRules.OrderByDescending(t => t.Item2).ToList();

                foreach (Tuple<string, int> rule in matchingRules)                          // insert characters based on matching rules
                {
                    template = template.Insert(rule.Item2, rule.Item1);
                }

                foreach (char letter in template.Distinct())                                // least and most populus characters
                {
                    int freq = Regex.Matches(template, letter.ToString()).Count;
                    results.Add(new Tuple<char, long>(letter, freq));
                    Console.Write(letter + "-" +  freq + " : ");
                }
                Console.WriteLine();
            }

            foreach (char letter in template.Distinct())                                    // least and most populus characters
            {
                int freq = Regex.Matches(template, letter.ToString()).Count;
                results.Add(new Tuple<char, long>(letter, freq));
            }

            results = results.OrderBy(t => t.Item2).ToList();

            return (results[results.Count() - 1].Item2 - results[0].Item2).ToString();
        }
    }
}