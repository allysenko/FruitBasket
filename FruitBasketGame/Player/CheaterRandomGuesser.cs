using FruitBasketGame.Game;
using System.Collections.Generic;

namespace FruitBasketGame.Player
{
    public class CheaterRandomGuesser : MemoryRandomGuesser
    {
        public CheaterRandomGuesser(string name)
            : base(name)
        { }

        protected override int GetNextNumber()
        {
            HashSet<int> alreadyTryedNumbers = GetUniqueAttempts(Referee.Attempts);
            NotTryedNumbers.RemoveAll((i) => alreadyTryedNumbers.Contains(i));
            
            return base.GetNextNumber();
        }

        private static HashSet<int> GetUniqueAttempts(IEnumerable<Attempt> attempts)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (var attempt in attempts)
                result.Add(attempt.Number);

            return result;
        }
    }
}
