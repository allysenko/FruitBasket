using System;

namespace FruitBasketGame.Player
{
    public class RandomGuesser : BasePlayer
    {
        public RandomGuesser(string name)
            :base(name)
        { }

        protected override void PrepareToGuess()
        {
        }
        protected override int GetNextNumber()
        {
            return new Random().Next(Rules.MinPossibleNumber, Rules.MaxPossibleNumber);
        }
    }
}
