using System;

namespace DiceLib
{
    public class DiceParseException : Exception
    {
        public DiceParseException(string diceInput)
            : base($"Error parsing dice input: {diceInput}")
        {
        }

        public DiceParseException(string diceInput, Exception inner)
            : base($"Error parsing dice input: {diceInput}", inner)
        {
        }
    }
}