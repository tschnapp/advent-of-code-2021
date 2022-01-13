using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Main
{
    class Day21 : Day
    {
        [TestCase("Test01", "739785")]
        [TestCase("Final", "576600")]
        public override string Part01(string[] rawInput)
        {
            var input = rawInput.ToList();
            var player1Pos = Convert.ToInt32(input[0].Substring(input[0].IndexOf(":") + 1));   // Player 1 starting position: 2
            var player2Pos = Convert.ToInt32(input[1].Substring(input[1].IndexOf(":") + 1));   // Player 1 starting position: 2
            var player1Score = 0;
            var player2Score = 0;
            var dDie = 0;
            var rollCount = 0;
            var addToScore = 0;

            do
            {
                addToScore = 0;                                     // Player 1s turn
                for (int i = 1; i <= 3; i++)
                {
                    rollCount++;
                    dDie++;
                    if (dDie > 100)
                        dDie = 1;
                    addToScore += dDie;
                }

                addToScore = (addToScore + player1Pos) % 10 == 0 ? 10 : (addToScore + player1Pos) % 10;
                player1Pos = addToScore;
                player1Score += addToScore;

                if (player1Score >= 1000)
                    return (player2Score * rollCount).ToString();

                addToScore = 0;                                     // Player 2s turn
                for (int i = 1; i <= 3; i++)
                {
                    rollCount++;
                    dDie++;
                    if (dDie > 100)
                        dDie = 1;
                    addToScore += dDie;
                }

                addToScore = (addToScore + player2Pos) % 10 == 0 ? 10 : (addToScore + player2Pos) % 10;
                player2Pos = addToScore;
                player2Score += addToScore;

                if (player2Score >= 1000)
                    return (player1Score * rollCount).ToString();

            } while (rollCount < 1000);

            return "";
        }

        //[TestCase("Test01", "?")]
        //[TestCase("Final", "?")]
        public override string Part02(string[] rawInput)
        {
            var input = rawInput.ToList();
            var player1Pos = Convert.ToInt32(input[0].Substring(input[0].IndexOf(":") + 1));   // Player 1 starting position: 2
            var player2Pos = Convert.ToInt32(input[1].Substring(input[1].IndexOf(":") + 1));   // Player 1 starting position: 2
            var player1Score = 0;
            var player2Score = 0;
            var dDie = 0;
            var rollCount = 0;
            var addToScore = 0;

            do
            {
                addToScore = 0;                                     // Player 1s turn
                for (int i = 1; i <= 3; i++)
                {
                    rollCount++;
                    dDie++;
                    if (dDie > 100)
                        dDie = 1;
                    addToScore += dDie;
                }

                addToScore = (addToScore + player1Pos) % 10 == 0 ? 10 : (addToScore + player1Pos) % 10;
                player1Pos = addToScore;
                player1Score += addToScore;

                if (player1Score >= 21)
                    return (player2Score * rollCount).ToString();

                addToScore = 0;                                     // Player 2s turn
                for (int i = 1; i <= 3; i++)
                {
                    rollCount++;
                    dDie++;
                    if (dDie > 100)
                        dDie = 1;
                    addToScore += dDie;
                }

                addToScore = (addToScore + player2Pos) % 10 == 0 ? 10 : (addToScore + player2Pos) % 10;
                player2Pos = addToScore;
                player2Score += addToScore;

                if (player2Score >= 1000)
                    return (player1Score * rollCount).ToString();

            } while (rollCount < 21);

            return "";
        }
    }
}