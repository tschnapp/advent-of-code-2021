using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day04 : Day
    {
        [TestCase("Test01", "4512")]
        [TestCase("Final", "82440")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var bingoInput = input[0].Split(',').Select(Int32.Parse).ToList();
            List<List<int>> cards = new List<List<int>> { new List<int> { } };

            // Get data
            var currentCard = 0;
            for (int l = 2; l < input.Count(); l++)
            {
                if (l % 6 == 1)
                {
                    cards.Add(new List<int> { });
                    currentCard++;
                }
                else
                {
                    cards[currentCard].AddRange(input[l].Trim().Replace("  ", " ").Split(' ').Select(Int32.Parse));
                }
            }

            // Create Checks list
            List<List<int>> checks = new List<List<int>> { };

            for (int c = 0; c < cards.Count; c++)
                checks.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });


            // Mark cards with number
            for (int n = 0; n < bingoInput.Count(); n++)
            {
                for (int c = 0; c < cards.Count(); c++)
                {
                    var numberPositionOnCard = cards[c].FindIndex(a => a == bingoInput[n]);
                    if (numberPositionOnCard != -1)
                    {
                        checks[c][numberPositionOnCard] = 1;

                        // Check if card is a winner
                        if (checkForWinner(checks[c]))
                            return (getWinnerTotal(checks[c], cards[c]) * cards[c][numberPositionOnCard]).ToString();
                    }
                }
            }

            return "";
        }

        private bool checkForWinner(List<int> cardChecks)
        {
            var isWinner = false;

            // Check horizontal rows
            for (int n = 0; n < cardChecks.Count(); n += 5)
                if (cardChecks.GetRange(n, 5).Sum() == 5)
                    isWinner = true;

            // Check vertical rows
            for (int n = 0; n < Math.Sqrt(cardChecks.Count()); n++)
                if (cardChecks.Where((v, i) => i % 5 == n).Sum() == 5)
                    isWinner = true;

            return isWinner;
        }

        private int getWinnerTotal(List<int> cardChecks, List<int> cardNumbers)
        {
            var total = 0;

            for(int n =0; n < cardChecks.Count(); n++)
                if (cardChecks[n] != 1)
                    total += cardNumbers[n];

            return total;
        }

        [TestCase("Test01", "1924")]
        [TestCase("Final", "20774")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var bingoInput = input[0].Split(',').Select(Int32.Parse).ToList();
            List<List<int>> cards = new List<List<int>> { new List<int> { } };
            List<int> winners = new List<int> { };

            // Get data
            var currentCard = 0;
            for (int l = 2; l < input.Count(); l++)
            {
                if (l % 6 == 1)
                {
                    cards.Add(new List<int> { });
                    currentCard++;
                }
                else
                {
                    cards[currentCard].AddRange(input[l].Trim().Replace("  ", " ").Split(' ').Select(Int32.Parse));
                }
            }

            // Create Checks list
            List<List<int>> checks = new List<List<int>> { };

            for (int c = 0; c < cards.Count; c++)
                checks.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });


            // Mark cards with number
            for (int n = 0; n < bingoInput.Count(); n++)
            {
                for (int c = 0; c < cards.Count(); c++)
                {
                    if (!winners.Contains(c))
                    {
                        var numberPositionOnCard = cards[c].FindIndex(a => a == bingoInput[n]);
                        if (numberPositionOnCard != -1)
                        {
                            checks[c][numberPositionOnCard] = 1;

                            // Check if card is a winner
                            if (checkForWinner(checks[c]))
                            {
                                winners.Add(c);
                                if (winners.Count() == cards.Count())
                                    return (getWinnerTotal(checks[c], cards[c]) * cards[c][numberPositionOnCard]).ToString();
                            }
                        }
                    }
                }
            }

            return "";
        }
    }
}