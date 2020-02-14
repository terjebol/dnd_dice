namespace DiceLib
{
    public class Modifier
    {
        public int Value { get; }


        public Modifier(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value < 0 ? Value.ToString() : "+" + Value;
        }
    }
}