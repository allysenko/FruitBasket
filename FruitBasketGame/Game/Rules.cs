using System;

namespace FruitBasketGame.Game
{
    public struct Rules
    {
        public int MinPossibleNumber { get; private set; }
        public int MaxPossibleNumber { get; private set; }
        public int MaxAttempts { get; private set; }
        public int MaxGameDurationMs { get; private set; }

        public Rules(int minPossibleNumber, int maxPossibleNumber, int maxAttempts, int maxGameDurationMs)
        {
            MinPossibleNumber = minPossibleNumber;
            MaxPossibleNumber = maxPossibleNumber;
            MaxAttempts = maxAttempts;
            MaxGameDurationMs = maxGameDurationMs;
        }
    }
}
