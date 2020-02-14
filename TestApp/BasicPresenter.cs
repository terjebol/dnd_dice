using System;
using System.Collections.Generic;
using System.Linq;
using DiceLib;

namespace TestApp
{
    public class BasicPresenter : IDiceRollPresenter
    {
        public void Present(IEnumerable<DieRoll> dieRolls)
        {
            var text = dieRolls.Select(x => x.ReadableInput).ToList();
            var results = dieRolls.Select(x => x.Result).ToList();
            text.ForEach(x => Console.Write(x+"\t"));
            Console.WriteLine();
            results.ForEach(x => Console.Write(x+"\t"));
            Console.WriteLine();
        }
    }
}