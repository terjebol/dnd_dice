using System.Collections.Generic;

namespace DiceLib
{
    public interface IDiceRollPresenter
    {
        void Present(IEnumerable<DieRoll> dieRolls);
    }
}