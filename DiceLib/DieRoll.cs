namespace DiceLib
{
    public class DieRoll
    {
        public int Result { get; }
        public string ReadableInput { get; }

        public DieRoll(int result, string readableInput)
        {
            Result = result;
            ReadableInput = readableInput;
        }
    }
}