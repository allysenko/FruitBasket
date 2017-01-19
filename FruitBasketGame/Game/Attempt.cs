using FruitBasketGame.Player;

namespace FruitBasketGame.Game
{
    public struct Attempt
    {
        public int Number { get; private set; }
        public IPlayer Player { get; private set; }

        public Attempt(IPlayer player, int number)
        {
            Number = number;
            Player = player;
        }
    }
}
