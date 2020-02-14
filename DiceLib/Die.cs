namespace DiceLib
{
    public class Die
    {
        public int NumDice { get; }
        public int DieSize { get; }
        private readonly string _input;
      

        public Die(string input, int numDice, int dieSize)
        {
            NumDice = numDice;
            DieSize = dieSize;
            _input = input;
        }
        
        public override string ToString()
        {
            return _input;
        }
        
        internal static Die Parse(string die)
        {
            return DiceParser.ParseDie(die);
        }
    }
}