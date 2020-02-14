using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceLib
{
    public class DiceBag
    {
        private static Random _rnd = new Random();
        private List<Die> _dice;
        private List<Modifier> _modifiers;

        internal static DiceBag Create(IEnumerable<DieContainer> containers)
        {
            containers = containers.ToList();
            var dice = containers
                .Where(x => !x.IsModifier)
                .Select(y => Die.Parse(y.Statement));

            var bonuses = containers
                .Where(x => x.IsModifier)
                .Select(y => new Modifier(int.Parse(y.Statement)));
            
            return new DiceBag
            {
                _dice = new List<Die>(dice),
                _modifiers = new List<Modifier>(bonuses)
            };
        }

        public static DiceBag Create(string input)
        {
            return DiceParser.Parse(input);
        }

        public static implicit operator DiceBag(string input) => Create(input);

        public static void SetSeed(int seed)
        {
            _rnd = new Random(seed);
        }

        public static DiceBag Create(IEnumerable<Die> dice, IEnumerable<Modifier> modifiers)
        {
            return new DiceBag
            {
                _dice = new List<Die>(dice),
                _modifiers = new List<Modifier>(modifiers)
            };
        }

        public override string ToString()
        {
            var arr = _dice
                .Select(x => x.ToString())
                .Concat(_modifiers.Select(x => x.ToString())).ToArray();
            return string.Join("", arr);
        }

        public int Roll(IDiceRollPresenter presenter)
        {
            var dieRolls = _dice.Select(x =>
                    new DieRoll(x.NumDice * (1 + _rnd.Next(x.DieSize)), x.ToString()))
                .Concat(_modifiers.Select(x => new DieRoll(x.Value, x.ToString()))).ToList();

            presenter?.Present(dieRolls);
            return dieRolls.Sum(x => x.Result);
        }
    }
}