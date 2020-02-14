namespace DiceLib
{
    public class DieContainer
    {
        public string Statement { get; }
        public bool IsModifier { get; }

        public DieContainer(string statement, bool isModifier)
        {
            Statement = statement;
            IsModifier = isModifier;
        }
    }
}