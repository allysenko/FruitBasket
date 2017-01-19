namespace FruitBasketGame.Player
{
    public class ThoroughGuesser : MemoryPlayer
    {
        public ThoroughGuesser(string name)
            :base(name)
        { }

        protected override int GetIndexToExtractNumber()
        {
            return 0;
        }
    }
}
