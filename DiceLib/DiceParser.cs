using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceLib
{
    internal static class DiceParser
    {
        private static bool IsNumber(char txt)
        {
            return "1234567890".Contains(txt);
        }

        private static int DiceCharIndex(string txt)
        {
            return txt.IndexOf('d');
        }

        private static string GetFormattedInput(string input)
        {
            input = input.ToLowerInvariant().Trim();
            while (input.Contains(" "))
            {
                input = input.Replace(" ", "");
            }

            return input;
        }

        private static bool IsSign(char ch)
        {
            return @"+-".Contains(ch);
        }

        private static IEnumerable<string> CreateDiceList(string input)
        {
            var diceList = new List<string>();
            var sb = new StringBuilder();
            foreach (var ch in input)
            {
                if (IsSign(ch))
                {
                    var dice = sb.ToString();
                    if (!string.IsNullOrEmpty(dice))
                    {
                        diceList.Add(dice);
                        sb.Clear();
                    }
                }
                sb.Append(ch);
            }
            diceList.Add(sb.ToString());
            return diceList;
        }

        private static bool IsInputLegal(string input)
        {
            const string validInput = "1234567890d+-()";
            return input.All(x => validInput.Contains(x));
        }

        internal static DiceBag Parse(string input)
        {
            input = GetFormattedInput(input);
            if (!IsInputLegal(input))
                throw new DiceParseException(input);
            
            var diceList = CreateDiceList(input);
            return DiceBag.Create(diceList.Select(ParseDieInput));
        }

        private static DieContainer ParseDieInput(string dieInput)
        {
            // ensure we dont have multiple d's like 2d2d2d2 2dd6 etc.
            if (dieInput.IndexOf('d') != dieInput.LastIndexOf('d'))
            {
                throw new DiceParseException(dieInput);
            }
            // The fragment is not a dice roll, but just a plain modifier
            if (int.TryParse(dieInput, out var numberFragment))
            {
                return new DieContainer(dieInput, true);
            }
            var signed = IsSign(dieInput[0]);
            var index = DiceCharIndex(dieInput);
        
            if (index == 0 || !IsNumber(dieInput[index - 1]))
            {
                dieInput = dieInput.Insert(index, "1");
                index = DiceCharIndex(dieInput);
            }
            
            if (index == dieInput.Length - 1 || !IsNumber(dieInput[index + 1]))
            {
                dieInput = dieInput.Insert(index + 1, "6");
            }

            return signed ? 
                new DieContainer(dieInput.Insert(dieInput.Length, ")").Insert(1, "("), false) : 
                new DieContainer(dieInput, false);
        }
        
        // At this point we know that the format of the input text is
        // fex 1d6, (1d6) or -(1d6)
        internal static Die ParseDie(string die)
        {
            var parsedDie = die.Replace("(", "").Replace(")","");
            var parts = parsedDie.Split("d");
            var numDice = int.Parse(parts[0]);
            var dieSize = int.Parse(parts[1]);
            return new Die(die, numDice, dieSize);
        }
    }
}