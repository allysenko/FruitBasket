namespace FruitBasketGame.Player
{
    public class CheaterThoroughGuesser : CheaterRandomGuesser
    {
        public CheaterThoroughGuesser(string name)
            :base(name)
        { }

        protected override int GetIndexToExtractNumber()
        {
            return 0;
        }
    }
}
