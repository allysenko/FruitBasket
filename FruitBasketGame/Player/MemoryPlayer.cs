using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitBasketGame.Player
{
    public abstract class MemoryPlayer : BasePlayer
    {
        protected List<int> NotTryedNumbers;

        protected MemoryPlayer(string name)
            :base(name)
        { }

        protected override void PrepareToGuess()
        {
            NotTryedNumbers = Enumerable.Range(Rules.MinPossibleNumber, Rules.MaxPossibleNumber).ToList();
        }

        protected override int GetNextNumber()
        {
            if (!NotTryedNumbers.Any())
                throw new ApplicationException("The referee is cheater");

            int index = GetIndexToExtractNumber();

            int number = NotTryedNumbers[index];
            NotTryedNumbers.RemoveAt(index);
            return number;
        }

        protected abstract int GetIndexToExtractNumber();
    }
}
