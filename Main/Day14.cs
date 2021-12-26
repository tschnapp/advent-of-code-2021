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
        //[TestCase("Test01", "1588")]
        //[TestCase("Final", "2549")]
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
        [TestCase("Final", "2516901104210")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var template = input[0];
            var rulesInput = input.GetRange(2, input.Count() - 2).Select(x => x.Split(" -> ")).ToList();
            var rules = new Dictionary<string, string>();
            var letters = "";

            foreach (string[] ruleInput in rulesInput)                  // extract rules
            {
                rules.Add(ruleInput[0], ruleInput[1]);
                letters += ruleInput[0];                                // while i'm here get all the letters
            }

            letters = new String(letters.Distinct().ToArray());         // reduce to distinct letters.

            var totalCounts = new Dictionary<string, long>();

            foreach (KeyValuePair<string, string> rule in rules)        // create and set default value for totalCounts
                totalCounts.Add(rule.Key, 0);

            for (int i = 0; i < template.Length - 1; i++)               // populate totalCounts
                totalCounts[template.Substring(i, 2)]++;

            for (int l = 0; l < 40; l++)                                // main loop, iterate 40 times
            {
                var iterationCounts = new Dictionary<string, long>(new Dictionary<string, long>(totalCounts));      // reset the iteration counts to the total counts as the starting point for this iteration

                foreach (KeyValuePair<string, long> item in iterationCounts)
                {
                    if (item.Value > 0)                                 // for each pair that have more than 0...
                    {
                        totalCounts[item.Key] -= item.Value;            // remove that pair
                        var itemsToAdd = new List<string>() { item.Key[0] + rules[item.Key], rules[item.Key] + item.Key[1] };
                        totalCounts[itemsToAdd[0]] += item.Value;       // add its first derived pair
                        totalCounts[itemsToAdd[1]] += item.Value;       // add its second derived pair
                    }
                }
            }

            var letterCounts = new Dictionary<char, long>();            // create letterCounts to hold letter totals

            foreach (char c in letters)                                 // populate with letter
                letterCounts.Add(c, 0);

            foreach (KeyValuePair<string, long> kvp in totalCounts)     // add letters from pairs, totalCounts
            {
                letterCounts[kvp.Key[0]] += kvp.Value;
                letterCounts[kvp.Key[1]] += kvp.Value;
            }

            letterCounts[template[0]]++;                                // add unconuted ends
            letterCounts[template[template.Length - 1]]++;

            List<char> keys = letterCounts.Keys.ToList();               // get dictionary key to iterate over dictionary and change values

            foreach (char key in keys)                                  // iterate over dictionary and divide by two since letters were double counted
                letterCounts[key] /= 2;

            var results = letterCounts.Values.Max() - letterCounts.Values.Min();    // calculte (max letter count - min letter count)

            return results.ToString();
        }
    }
}