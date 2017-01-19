using System;

namespace FruitBasketGame.Player
{
    public class MemoryRandomGuesser : MemoryPlayer
    {
        public MemoryRandomGuesser(string name)
            :base(name)
        { }

        protected override int GetIndexToExtractNumber()
        {
            return new Random().Next(0, NotTryedNumbers.Count);
        }
    }
}
