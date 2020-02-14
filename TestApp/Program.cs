using System;
using System.Collections.Generic;
using DiceLib;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DiceBag toHit = "1d20 + 4";
            var summedResult = toHit.Roll(new BasicPresenter());
            Console.WriteLine("Final result: "+summedResult);
            
            DiceBag damage = DiceBag.Create(
                new List<Die> {new Die("1d6", 1, 6)},
                new List<Modifier> {new Modifier(2)});
            summedResult = damage.Roll(new BasicPresenter());
            Console.WriteLine("Final result: "+summedResult);

            try
            {
                DiceBag error = "-12dd";
            }
            catch (DiceParseException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}